namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class CreateInventoryReceiptEntriesCommandHandler : IRequestHandler<CreateInventoryReceiptEntriesCommand, List<string>>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IReceiptLotRepository _receiptLotRepository;

        public CreateInventoryReceiptEntriesCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository, IMaterialRepository materialRepository, IReceiptLotRepository receiptLotRepository)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _materialRepository = materialRepository;
            _receiptLotRepository = receiptLotRepository;
        }

        public async Task<List<string>> Handle(CreateInventoryReceiptEntriesCommand request, CancellationToken cancellationToken)
        {
            var inventoryReceipt = await _inventoryReceiptRepository.GetByReceiptIdAsync(request.InventoryReceiptId)
                                ?? throw new EntityNotFoundException(nameof(InventoryReceipt), request.InventoryReceiptId);

            if (inventoryReceipt.IsDone())
                throw new Exception("The Receipt has been saved");

            var newEntryIds = new List<string>();
            foreach (var entry in request.Entries)
            {
                var newEntry = await CreateInventoryReceiptEntry(entry, request.InventoryReceiptId);
                inventoryReceipt.AddEntry(newEntry);

                newEntryIds.Add(newEntry.InventoryReceiptEntryId);
            }

            await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return newEntryIds;
        }

        private async Task<InventoryReceiptEntry> CreateInventoryReceiptEntry(CreateReceiptEntryCommand request, string InventoryReceiptId)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(request.MaterialId)
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
