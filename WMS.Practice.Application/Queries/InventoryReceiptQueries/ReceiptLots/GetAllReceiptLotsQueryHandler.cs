namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.ReceiptLots
{
    public class GetAllReceiptLotsQueryHandler : IRequestHandler<GetAllReceiptLotsQuery, IEnumerable<ReceiptLotDTO>>
    {
        private readonly IReceiptLotRepository _receiptLotRepository;
        private readonly IInventoryReceiptEntryRepository _inventoryReceiptEntryRepository;
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetAllReceiptLotsQueryHandler(IReceiptLotRepository receiptLotRepository, IInventoryReceiptEntryRepository inventoryReceiptEntryRepository, IInventoryReceiptRepository inventoryReceiptRepository, IMaterialRepository materialRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _receiptLotRepository = receiptLotRepository;
            _inventoryReceiptEntryRepository = inventoryReceiptEntryRepository;
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReceiptLotDTO>> Handle(GetAllReceiptLotsQuery request, CancellationToken cancellationToken)
        {
            var receiptLots = await _receiptLotRepository.GetAllAsync()
                           ?? throw new EntityNotFoundException($"Receipt Lots could not found");

            var inventoryReceiptEntries = await _inventoryReceiptEntryRepository.GetAllInventoryReceiptEntriesAsync();

            var receiptLotsDTOs = _mapper.Map<IEnumerable<ReceiptLotDTO>>(receiptLots);
            foreach (var receiptLotsDTO in receiptLotsDTOs)
            {
                foreach (var inventoryReceiptEntry in inventoryReceiptEntries)
                {
                    if (receiptLotsDTO.InventoryReceiptEntryId == inventoryReceiptEntry.InventoryReceiptEntryId)
                    {
                        var material = await _materialRepository.GetMaterialByIdAsync(inventoryReceiptEntry.MaterialId)
                                    ?? throw new EntityNotFoundException($"Material with Id {inventoryReceiptEntry.MaterialId} could not found");

                        receiptLotsDTO.MaterialId = material.MaterialId;
                        receiptLotsDTO.MaterialName = material.MaterialName;

                        if (material.TryGetStorageLevel(out int storageLevel))
                            receiptLotsDTO.StorageLevel = storageLevel.ToString();

                        var inventoryReceipt = await _inventoryReceiptRepository.GetInventoryReceiptByReceiptIdAsync(inventoryReceiptEntry.InventoryReceiptId)
                                            ?? throw new EntityNotFoundException($"Inventory Receipt with Id {inventoryReceiptEntry.InventoryReceiptId} could not found");

                        var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(inventoryReceipt.WarehouseId)
                                     ?? throw new EntityNotFoundException($"Warehouse with Id {inventoryReceipt.WarehouseId} could not found");

                        receiptLotsDTO.WarehouseId = warehouse.WarehouseId;
                        receiptLotsDTO.WarehouseName = warehouse.WarehouseName;
                    }
                }
            }

            return receiptLotsDTOs;
        }

    }
}
