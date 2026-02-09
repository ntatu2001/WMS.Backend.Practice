using WMS.Practice.Application.Services.InventoryLogs.InventoryReceipts;

namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class UpdateInventoryReceiptEntryCommandHandler : IRequestHandler<UpdateInventoryReceiptEntryCommand, bool>
    {
        private IInventoryReceiptRepository _inventoryReceiptRepository;
        private IMaterialRepository _materialRepository;
        private IReceiptLoggingService _receiptLoggingService;

        public UpdateInventoryReceiptEntryCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository, IMaterialRepository materialRepository, IReceiptLoggingService receiptLoggingService)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _materialRepository = materialRepository;
            _receiptLoggingService = receiptLoggingService;
        }

        public async Task<bool> Handle(UpdateInventoryReceiptEntryCommand request, CancellationToken cancellationToken)
        {
            var inventoryReceipt = await _inventoryReceiptRepository.GetByReceiptIdAsync(request.InventoryReceiptId)
                                ?? throw new EntityNotFoundException(nameof(InventoryReceipt), request.InventoryReceiptId);

            if (inventoryReceipt.IsDone())
                throw new Exception("The Receipt has been saved");

            var entry = inventoryReceipt.FindEntry(request.InventoryReceiptEntryId)
                     ?? throw new EntityNotFoundException(nameof(InventoryReceiptEntry), request.InventoryReceiptEntryId);

            string materialId = request.MaterialId is not null ? request.MaterialId : entry.MaterialId;
            var material = await _materialRepository.GetByMaterialIdAsync(materialId)
                        ?? throw new EntityNotFoundException(nameof(Material), materialId);

            entry.Update(purchaseOrderNumber: request.PurchaseOrderNumber ?? entry.PurchaseOrderNumber,
                         materialId: request.MaterialId ?? material.MaterialId,
                         materialName: request.MaterialName ?? material.MaterialName,
                         note: request.Note ?? entry.Note,
                         importedQuantity: request.ImportedQuantity ?? entry.ImportedQuantity,
                         lotNumber: request.LotNumber ?? entry.LotNumber,
                         status: request.ReceiptLotStatus?.ParseEnum<LotStatus>());


            await _receiptLoggingService.UpdateInventoryLog(inventoryReceipt);
            return await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
