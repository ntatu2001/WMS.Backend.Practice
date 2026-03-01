namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.ReceiptLots
{
    public class GetReceiptLotByIdQueryHandler : IRequestHandler<GetReceiptLotByIdQuery, ReceiptLotDTO>
    {
        private readonly IReceiptLotRepository _receiptLotRepository;
        private readonly IInventoryReceiptEntryRepository _inventoryReceiptEntryRepository;
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetReceiptLotByIdQueryHandler(IReceiptLotRepository receiptLotRepository, IInventoryReceiptEntryRepository inventoryReceiptEntryRepository, IInventoryReceiptRepository inventoryReceiptRepository, IMaterialRepository materialRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _receiptLotRepository = receiptLotRepository;
            _inventoryReceiptEntryRepository = inventoryReceiptEntryRepository;
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<ReceiptLotDTO> Handle(GetReceiptLotByIdQuery request, CancellationToken cancellationToken)
        {
            var receiptLot = await _receiptLotRepository.GetReceiptLotByIdAsync(request.ReceiptLotId)
                          ?? throw new EntityNotFoundException(nameof(ReceiptLot), request.ReceiptLotId);

            var entry = await _inventoryReceiptEntryRepository.GetByReceiptLotId(receiptLot.ReceiptLotId)
                     ?? throw new EntityNotFoundException(nameof(InventoryReceiptEntry), receiptLot.ReceiptLotId);

            var receiptLotDTO = _mapper.Map<ReceiptLotDTO>(receiptLot);

            var material = await _materialRepository.GetMaterialByIdAsync(entry.MaterialId)
                        ?? throw new EntityNotFoundException($"Material with Id {entry.MaterialId} could not found");

            receiptLotDTO.MaterialName = material.MaterialName;
            receiptLotDTO.MaterialId = material.MaterialId;

            if (material.TryGetStorageLevel(out int storageLevel))
                receiptLotDTO.StorageLevel = storageLevel.ToString();

            var inventoryReceipt = await _inventoryReceiptRepository.GetInventoryReceiptByReceiptIdAsync(entry.InventoryReceiptId)
                                ?? throw new EntityNotFoundException($"Inventory Receipt with Id {entry.InventoryReceiptId} could not found");

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(inventoryReceipt.WarehouseId)
                         ?? throw new EntityNotFoundException($"Warehouse with Id {inventoryReceipt.WarehouseId} could not found");

            receiptLotDTO.WarehouseId = warehouse.WarehouseId;
            receiptLotDTO.WarehouseName = warehouse.WarehouseName;

            return receiptLotDTO;
        }


    }
}
