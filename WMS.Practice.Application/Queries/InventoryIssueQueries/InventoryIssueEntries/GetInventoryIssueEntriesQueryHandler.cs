namespace WMS.Practice.Application.Queries.InventoryIssueQueries.InventoryIssueEntries
{
    public class GetInventoryIssueEntriesQueryHandler : IRequestHandler<GetInventoryIssueEntriesQuery, QueryResult<InventoryIssueEntryDTO>>
    {
        private readonly IInventoryIssueEntryRepository _inventoryIssueEntryRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetInventoryIssueEntriesQueryHandler(IInventoryIssueEntryRepository inventoryIssueEntryRepository, IMaterialRepository materialRepository,
                                                       IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _inventoryIssueEntryRepository = inventoryIssueEntryRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<QueryResult<InventoryIssueEntryDTO>> Handle(GetInventoryIssueEntriesQuery request, CancellationToken cancellationToken)
        {
            var entriesQuery = _inventoryIssueEntryRepository.QueryInventoryIssueEntries();

            if (request.FromDate.HasValue)
            {
                entriesQuery = entriesQuery.Where(e => e.InventoryIssue.IssueDate.Date >= request.FromDate.Value.Date);
            }

            if (request.ToDate.HasValue)
            {
                entriesQuery = entriesQuery.Where(e => e.InventoryIssue.IssueDate.Date <= request.ToDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(request.WarehouseName))
            {
                var matchingWarehouseIds = await _warehouseRepository.GetWarehouseIdByWarehouseNameAsync(request.WarehouseName);
                entriesQuery = entriesQuery.Where(e => matchingWarehouseIds.Contains(e.InventoryIssue.WarehouseId));
            }

            if (!string.IsNullOrWhiteSpace(request.LotNumber))
            {
                entriesQuery = entriesQuery.Where(e => e.IssueLot.MaterialLotId.Contains(request.LotNumber));
            }

            if (!string.IsNullOrWhiteSpace(request.MaterialName))
            {
                entriesQuery = entriesQuery.Where(e => e.MaterialName.Contains(request.MaterialName));
            }

            // Progress order: InProgress - Pending - Done - HoldOn - IsBlocked - Cancelled
            entriesQuery = entriesQuery.OrderBy(e => e.IssueLot.LotStatus == LotStatus.InProgress ? 0
                                                    : e.IssueLot.LotStatus == LotStatus.Pending ? 1
                                                    : e.IssueLot.LotStatus == LotStatus.Done ? 2
                                                    : e.IssueLot.LotStatus == LotStatus.HoldOn ? 3
                                                    : e.IssueLot.LotStatus == LotStatus.IsBlocked ? 4
                                                    : 5)
                                        .ThenByDescending(e => e.InventoryIssue.IssueDate)
                                        .ThenBy(e => e.InventoryIssueEntryId);

            var totalItems = await entriesQuery.CountAsync(cancellationToken);

            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                var skip = (request.PageNumber.Value - 1) * request.PageSize.Value;
                entriesQuery = entriesQuery.Skip(skip).Take(request.PageSize.Value);
            }

            var pagedEntries = await entriesQuery.ToListAsync(cancellationToken);

            var inventoryIssueEntryDTOs = new List<InventoryIssueEntryDTO>();
            foreach (var inventoryIssueEntry in pagedEntries)
            {
                var inventoryIssue = inventoryIssueEntry.InventoryIssue;

                var inventoryIssueEntryDTO = _mapper.Map<InventoryIssueEntryDTO>(inventoryIssueEntry);

                var material = await _materialRepository.GetMaterialByIdAsync(inventoryIssueEntry.MaterialId)
                            ?? throw new EntityNotFoundException($"Material with Id {inventoryIssueEntry.MaterialId} could not found");

                inventoryIssueEntryDTO.PersonId = inventoryIssue.Employee.EmployeeId;
                inventoryIssueEntryDTO.WarehouseId = inventoryIssue.Warehouse.WarehouseId;
                if (material.TryGetUnitOfMeasure(out var unitValue))
                {
                    inventoryIssueEntryDTO.Unit = unitValue;
                }

                inventoryIssueEntryDTO.MapName(materialName: material.MaterialName,
                                               personName: inventoryIssue.Employee.EmployeeName,
                                               warehouseName: inventoryIssue.Warehouse.WarehouseName,
                                               lotNumber: inventoryIssueEntry.IssueLot.MaterialLotId,
                                               issueDate: inventoryIssue.IssueDate,
                                               unit: unitValue);

                inventoryIssueEntryDTOs.Add(inventoryIssueEntryDTO);
            }

            return new QueryResult<InventoryIssueEntryDTO>(results: inventoryIssueEntryDTOs, totalItems: totalItems);
        }
    }
}
