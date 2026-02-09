namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.ReceiptLots
{
    public class UpdateReceiptLotStatusCommandHandler : IRequestHandler<UpdateReceiptLotStatusCommand, bool>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IReceiptLotRepository _receiptLotRepository;
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMaterialSubLotRepository _materialSubLotRepository;

        public UpdateReceiptLotStatusCommandHandler(IInventoryReceiptRepository inventoryReceiptRepository, IReceiptLotRepository receiptLotRepository, 
                                                    IMaterialLotRepository materialLotRepository, IMaterialSubLotRepository materialSubLotRepository)
        {
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _receiptLotRepository = receiptLotRepository;
            _materialLotRepository = materialLotRepository;
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<bool> Handle(UpdateReceiptLotStatusCommand request, CancellationToken cancellationToken)
        {
            var receiptLot = await _receiptLotRepository.GetByIdAsync(request.ReceiptLotId)
                          ?? throw new EntityNotFoundException(nameof(ReceiptLot), request.ReceiptLotId);

            if (receiptLot.IsDone() || receiptLot.IsPending())
                throw new Exception($"Receipt lot {request.ReceiptLotId} is already Done or Pending.");

            // Update Receipt Lot Status sent from Command
            receiptLot.Update(lotStatus: request.ReceiptLotStatus.ParseEnum<LotStatus>());
            if (receiptLot.IsDone())
            {
                // If Receipt Lot is Done, create a new Material Lot with Sublots
                await CreateMaterialLotAsyncWhenReceiptLotIsDone(receiptLot);
            }

            // Save the Status of Receipt Lot and Material Lot (if created) from DBContext to MSSQL
            await _receiptLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var inventoryReceipt = await _inventoryReceiptRepository.GetByReceiptIdAsync(receiptLot.InventoryReceiptEntry.InventoryReceiptId)
                                ?? throw new EntityNotFoundException(nameof(InventoryReceipt), receiptLot.InventoryReceiptEntry.InventoryReceiptId);

            // Update the Status of Inventory Receipt based on Entries Status
            UpdateInventoryReceiptStatus(inventoryReceipt);

            // Save the Status of Inventory Receipt to MSSQL
            return await _inventoryReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task CreateMaterialLotAsyncWhenReceiptLotIsDone(ReceiptLot receiptLot)
        {
            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(receiptLot.ReceiptLotId)
                           ?? throw new DuplicateRecordException(nameof(MaterialLot), receiptLot.ReceiptLotId);

            var newMaterialLot = new MaterialLot(lotNumber: receiptLot.ReceiptLotId,
                                                 lotStatus: LotStatus.Done,
                                                 materialId: receiptLot.InventoryReceiptEntry.MaterialId,
                                                 existingQuantity: receiptLot.ImportedQuantity);

            foreach (var sublot in receiptLot.ReceiptSubLots)
            {
                if (await _materialSubLotRepository.ExistAsync(sublot.ReceiptSubLotId) is true)
                    throw new DuplicateRecordException(nameof(MaterialSubLot), sublot.ReceiptSubLotId);

                var newSubLot = new MaterialSubLot(materialSubLotId: sublot.ReceiptSubLotId,
                                                   subLotStatus: LotStatus.Done,
                                                   existingQuantity: sublot.ImportedQuantity,
                                                   unitOfMeasure: sublot.UnitOfMeasure,
                                                   locationId: sublot.LocationId,
                                                   lotNumber: sublot.ReceiptLotId);

                newMaterialLot.AddSubLot(newSubLot);
            }

            _materialLotRepository.Create(newMaterialLot);
        }

        private static void UpdateInventoryReceiptStatus(InventoryReceipt inventoryReceipt)
        {
            var isCompleted = inventoryReceipt.Entries.All(e => e.ReceiptLot.IsDone());

            // If all Entries of Inventory Receipt are DONE, change the status of Inventory Receipt to DONE.
            // If there is an Entry is not Done, keep InProgress for Inventory Receipt.
            inventoryReceipt.Update(receiptStatus: isCompleted
                                                 ? ReceiptStatus.Done
                                                 : ReceiptStatus.InProgress);
        }

    }
}
