namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.ReceiptSubLots
{
    public class UpdateReceiptSubLotsCommandHandler : IRequestHandler<UpdateReceiptSubLotsCommand, bool>
    {
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IReceiptLotRepository _receiptLotRepository;
        private const string defaultUnitPropertyName = "Unit";

        public UpdateReceiptSubLotsCommandHandler(IMaterialRepository materialRepository, IReceiptLotRepository receiptLotRepository, IInventoryReceiptRepository inventoryReceiptRepository)
        {
            _materialRepository = materialRepository;
            _receiptLotRepository = receiptLotRepository;
            _inventoryReceiptRepository = inventoryReceiptRepository;
        }

        /// <summary>
        /// API to create Receipt SubLots after implementing Storage Location Assignment Algorithm
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> Handle(UpdateReceiptSubLotsCommand request, CancellationToken cancellationToken)
        {
            // Group SubLots by Lot Number
            var subLotGroups = GroupSubLotsByLotNumber(request.ReceiptSubLots);
            foreach (var subLotGroup in subLotGroups)
            {
                // Retrieve Receipt Lot corresponding to Lot Number
                var receiptLot = await _receiptLotRepository.GetReceiptByLotId(subLotGroup.Key)
                              ?? throw new EntityNotFoundException(nameof(ReceiptLot), subLotGroup.Key);

                // Retrieve Inventory Receipt containing Receipt Lot
                var inventoryReceipt = await _inventoryReceiptRepository.GetByReceiptIdAsync(receiptLot.InventoryReceiptEntry.InventoryReceiptId)
                                    ?? throw new EntityNotFoundException(nameof(InventoryReceipt), receiptLot.InventoryReceiptEntry.InventoryReceiptId);

                foreach (var subLot in subLotGroup.Value)
                {
                    var material = await _materialRepository.GetMaterialByIdAsync(subLot.MaterialId)
                                ?? throw new EntityNotFoundException(nameof(Material), subLot.MaterialId);

                    // Try get property value of Unit to parse Enum
                    if (material.TryGetPropertyValue(defaultUnitPropertyName, out var unitPropertyValue) is false)
                        throw new ArgumentException($"Material Properties do not have {defaultUnitPropertyName} Property");

                    // Create a new Receipt SubLot with Status is InProgress
                    var receiptSubLot = new ReceiptSubLot(receiptSubLotId: subLot.ReceiptSubLotId,
                                                          importedQuantity: subLot.ImportedQuantity,
                                                          lotStatus: LotStatus.InProgress,
                                                          unitOfMeasure: unitPropertyValue.ParseEnum<UnitOfMeasure>(),
                                                          locationId: subLot.LocationId,
                                                          receiptLotId: subLotGroup.Key);

                    // Add new SubLot into Receipt Lot
                    receiptLot.AddSublot(receiptSubLot);
                }

                // Keep the Status of Receipt Lot to InProgress
                receiptLot.Update(lotStatus: LotStatus.InProgress);

                // Also, keep the Status of Inventory Receipt to InProgress
                inventoryReceipt.Update(receiptStatus: ReceiptStatus.InProgress);
            }

            return await _receiptLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private Dictionary<string, List<ReceiptSubLotNewDTO>> GroupSubLotsByLotNumber(IList<ReceiptSubLotNewDTO> receiptSubLots)
        {
            return receiptSubLots.GroupBy(x => x.LotNumber).ToDictionary(x => x.Key, y => y.ToList());
        }

    }
}
