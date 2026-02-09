namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceipts
{
    public class DeleteInventoryReceiptCommandHandler : IRequestHandler<DeleteInventoryReceiptCommand, bool>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;

        public DeleteInventoryReceiptCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
        }

        public async Task<bool> Handle(DeleteInventoryReceiptCommand request, CancellationToken cancellationToken)
        {
            var inventoryReceipt = await _inventoryReceiptRepository.GetByReceiptIdAsync(request.InventoryReceiptId)
                                ?? throw new EntityNotFoundException(nameof(InventoryReceipt), request.InventoryReceiptId);

            if (inventoryReceipt.IsDone())
                throw new Exception("The Receipt has been saved");

            _inventoryReceiptRepository.Delete(inventoryReceipt);
            return await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
