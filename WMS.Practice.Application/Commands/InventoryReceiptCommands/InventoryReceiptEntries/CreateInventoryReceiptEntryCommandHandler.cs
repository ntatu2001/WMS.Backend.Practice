namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class CreateInventoryReceiptEntryCommandHandler : IRequestHandler<CreateInventoryReceiptEntryCommand, bool>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IReceiptLotRepository _receiptLotRepository;

        public CreateInventoryReceiptEntryCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository, IMaterialRepository materialRepository, IReceiptLotRepository receiptLotRepository)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _materialRepository = materialRepository;
            _receiptLotRepository = receiptLotRepository;
        }

        public async Task<bool> Handle(CreateInventoryReceiptEntryCommand request, CancellationToken cancellationToken)
        {
            var inventoryReceipt = await _inventoryReceiptRepository.GetByReceiptIdAsync(request.InventoryReceiptId)
                                ?? throw new EntityNotFoundException(nameof(InventoryReceipt), request.InventoryReceiptId);

            if (inventoryReceipt.IsDone())
                throw new Exception("The Receipt has been saved");

            var newEntry = await CreateInventoryReceiptEntry(request, request.InventoryReceiptId);
            inventoryReceipt.AddEntry(newEntry);

            return await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task<InventoryReceiptEntry> CreateInventoryReceiptEntry(CreateInventoryReceiptEntryCommand request, string InventoryReceiptId)
        {
            var material = await _materialRepository.GetByMaterialIdAsync(request.MaterialId)
                        ?? throw new EntityNotFoundException(nameof(Material), request.MaterialId);

            if (await _receiptLotRepository.ExistAsync(request.LotNumber) is true)
                throw new DuplicateRecordException(nameof(ReceiptLot), request.LotNumber);

            var newEntry = new InventoryReceiptEntry(inventoryReceiptEntryId: Guid.NewGuid().ToString(),
                                                     purchaseOrderNumber: request.PurchaseOrderNumber ?? request.LotNumber,
                                                     materialId: request.MaterialId,
                                                     materialName: request.MaterialName ?? material.MaterialName,
                                                     note: request.Note ?? "None",
                                                     importedQuantity: request.ImportedQuantity ?? 0.0,
                                                     lotNumber: request.LotNumber,
                                                     inventoryReceiptId: InventoryReceiptId);

            newEntry.UpdateReceiptLot(new ReceiptLot(receiptLotId: newEntry.LotNumber,
                                                     importedQuantity: newEntry.ImportedQuantity,
                                                     lotStatus: LotStatus.Pending,
                                                     inventoryReceiptEntryId: newEntry.InventoryReceiptEntryId));
            return newEntry;
        }
    }
}
