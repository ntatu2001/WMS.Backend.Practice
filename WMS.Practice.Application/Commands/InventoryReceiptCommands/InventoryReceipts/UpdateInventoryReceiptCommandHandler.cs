namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceipts
{
    public class UpdateInventoryReceiptCommandHandler : IRequestHandler<UpdateInventoryReceiptCommand, bool>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IReceiptLoggingService _receiptLoggingService;

        public UpdateInventoryReceiptCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository, IReceiptLoggingService receiptLoggingService)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _receiptLoggingService = receiptLoggingService;
        }

        public async Task<bool> Handle(UpdateInventoryReceiptCommand request, CancellationToken cancellationToken)
        {
            var existingReceipt = await _inventoryReceiptRepository.GetByReceiptIdAsync(request.InventoryReceiptId)
                       ?? throw new EntityNotFoundException(nameof(InventoryReceipt), request.InventoryReceiptId);

            if (existingReceipt.IsDone())
                throw new InvalidOperationException("Inventory receipt has already been finalized.");

            var receiptStatus = request.Status?.ParseEnum<ReceiptStatus>()
                             ?? throw new ArgumentException("Receipt status is required.", nameof(request.Status));

            existingReceipt.Update(supplierId: request.SupplierId,
                                   employeeId: request.EmployeeId,
                                   warehouseId: request.WarehouseId,
                                   receiptStatus: receiptStatus);

            if (receiptStatus is ReceiptStatus.Done)
            {
                // If Receipt Status is updated as DONE, we have to confirm and raise Inventory Log for this Inventory Receipt
                await _receiptLoggingService.UpdateInventoryLog(existingReceipt);
            }

            return await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
