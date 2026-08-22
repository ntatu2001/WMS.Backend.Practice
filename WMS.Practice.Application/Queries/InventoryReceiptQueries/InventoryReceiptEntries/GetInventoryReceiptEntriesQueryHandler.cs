namespace WMS.Practice.Application.Queries.InventoryReceiptQueries.InventoryReceiptEntries
{
    public class GetInventoryReceiptEntriesQueryHandler : IRequestHandler<GetInventoryReceiptEntriesQuery, QueryResult<InventoryReceiptEntryDTO>>
    {
        private readonly IInventoryReceiptEntryRepository _inventoryReceiptEntryRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetInventoryReceiptEntriesQueryHandler(IInventoryReceiptEntryRepository inventoryReceiptEntryRepository, IMaterialRepository materialRepository,
                                                         IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _inventoryReceiptEntryRepository = inventoryReceiptEntryRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<QueryResult<InventoryReceiptEntryDTO>> Handle(GetInventoryReceiptEntriesQuery request, CancellationToken cancellationToken)
        {
            var entriesQuery = _inventoryReceiptEntryRepository.QueryInventoryReceiptEntries();

            if (request.FromDate.HasValue)
            {
                entriesQuery = entriesQuery.Where(e => e.InventoryReceipt.ReceiptDate.Date >= request.FromDate.Value.Date);
            }

            if (request.ToDate.HasValue)
            {
                entriesQuery = entriesQuery.Where(e => e.InventoryReceipt.ReceiptDate.Date <= request.ToDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(request.WarehouseName))
            {
                var matchingWarehouseIds = await _warehouseRepository.GetWarehouseIdByWarehouseNameAsync(request.WarehouseName);
                entriesQuery = entriesQuery.Where(e => matchingWarehouseIds.Contains(e.InventoryReceipt.WarehouseId));
            }

            if (!string.IsNullOrWhiteSpace(request.LotNumber))
            {
                entriesQuery = entriesQuery.Where(e => e.LotNumber.Contains(request.LotNumber));
            }

            if (!string.IsNullOrWhiteSpace(request.MaterialName))
            {
                entriesQuery = entriesQuery.Where(e => e.MaterialName.Contains(request.MaterialName));
            }

            // Progress order: InProgress - Pending - Done - HoldOn - IsBlocked - Cancelled
            entriesQuery = entriesQuery.OrderBy(e => e.ReceiptLot.LotStatus == LotStatus.InProgress ? 0
                                                    : e.ReceiptLot.LotStatus == LotStatus.Pending ? 1
                                                    : e.ReceiptLot.LotStatus == LotStatus.Done ? 2
                                                    : e.ReceiptLot.LotStatus == LotStatus.HoldOn ? 3
                                                    : e.ReceiptLot.LotStatus == LotStatus.IsBlocked ? 4
                                                    : 5)
                                        .ThenByDescending(e => e.InventoryReceipt.ReceiptDate)
                                        .ThenBy(e => e.InventoryReceiptEntryId);

            var totalItems = await entriesQuery.CountAsync(cancellationToken);

            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                var skip = (request.PageNumber.Value - 1) * request.PageSize.Value;
                entriesQuery = entriesQuery.Skip(skip).Take(request.PageSize.Value);
            }

            var pagedEntries = await entriesQuery.ToListAsync(cancellationToken);

            var inventoryReceiptEntriesDTOs = new List<InventoryReceiptEntryDTO>();
            foreach (var inventoryReceiptEntry in pagedEntries)
            {
                var inventoryReceipt = inventoryReceiptEntry.InventoryReceipt;

                var inventoryReceiptEntryDTO = _mapper.Map<InventoryReceiptEntryDTO>(inventoryReceiptEntry);
                inventoryReceiptEntryDTO.ReceiptLot = _mapper.Map<ReceiptLotDTO>(inventoryReceiptEntry.ReceiptLot);

                var material = await _materialRepository.GetMaterialByIdAsync(inventoryReceiptEntry.MaterialId)
                            ?? throw new EntityNotFoundException($"Material with Id {inventoryReceiptEntry.MaterialId} could not found");

                if (material.TryGetPropertyValue("Unit", out var unitValue))
                {
                    inventoryReceiptEntryDTO.Unit = unitValue;
                }

                inventoryReceiptEntryDTO.MapName(material.MaterialName, warehouseName: inventoryReceipt.Warehouse.WarehouseName, personName: inventoryReceipt.Employee.EmployeeName);
                inventoryReceiptEntryDTO.ReceiptDate = inventoryReceipt.ReceiptDate;
                inventoryReceiptEntriesDTOs.Add(inventoryReceiptEntryDTO);
            }

            return new QueryResult<InventoryReceiptEntryDTO>(results: inventoryReceiptEntriesDTOs, totalItems: totalItems);
        }
    }
}
