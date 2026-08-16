namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.ReceiptLots
{
    public class GetReceiptLotsPendingQueryHandler : IRequestHandler<GetReceiptLotsPendingQuery, IEnumerable<ReceiptLotDTO>>
    {
        private readonly IReceiptLotRepository _receiptLotRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetReceiptLotsPendingQueryHandler(IReceiptLotRepository receiptLotRepository, IMaterialRepository materialRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _receiptLotRepository = receiptLotRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReceiptLotDTO>> Handle(GetReceiptLotsPendingQuery request, CancellationToken cancellationToken)
        {
            var receiptLots = await _receiptLotRepository.GetReceiptLotsAsPending()
                           ?? throw new EntityNotFoundException(nameof(ReceiptLot), "No receipt lots found with status Pending");

            var receiptLotsDTOs = new List<ReceiptLotDTO>();
            foreach (var receiptLot in receiptLots)
            {
                var receiptLotsDTO = _mapper.Map<ReceiptLotDTO>(receiptLot);

                var material = await _materialRepository.GetMaterialByIdAsync(receiptLot.InventoryReceiptEntry.MaterialId);
                if (material is not null)
                {
                    receiptLotsDTO.MaterialId = material.MaterialId;
                    receiptLotsDTO.MaterialName = material.MaterialName;
                    if (material.TryGetStorageLevel(out int storageLevel))
                    {
                        receiptLotsDTO.StorageLevel = storageLevel.ToString();
                    }

                    var (warehouseId, warehouseName) = await _warehouseRepository.GetWarehouseNameAndIdByIdAsync(receiptLot.InventoryReceiptEntry.InventoryReceipt.WarehouseId)
                                                    ?? throw new Exception($"Warehouse not found for ID {receiptLot.InventoryReceiptEntry.InventoryReceipt.WarehouseId}");

                    receiptLotsDTO.WarehouseId = warehouseId;
                    receiptLotsDTO.WarehouseName = warehouseName;
                    receiptLotsDTOs.Add(receiptLotsDTO);
                }
            }

            return receiptLotsDTOs.Where(x => x.WarehouseId == request.WarehouseId);
        }
    }
}
