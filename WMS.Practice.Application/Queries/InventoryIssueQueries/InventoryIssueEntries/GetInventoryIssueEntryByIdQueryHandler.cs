namespace WMS.Practice.Application.Queries.InventoryIssueQueries.InventoryIssueEntries
{
    public class GetInventoryIssueEntryByIdQueryHandler : IRequestHandler<GetInventoryIssueEntryByIdQuery, InventoryIssueEntryDTO>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IInventoryIssueEntryRepository _inventoryIssueEntryRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetInventoryIssueEntryByIdQueryHandler(IInventoryIssueRepository inventoryIssueRepository, IInventoryIssueEntryRepository inventoryIssueEntryRepository, IMaterialRepository materialRepository, IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _inventoryIssueEntryRepository = inventoryIssueEntryRepository;
            _materialRepository = materialRepository;
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<InventoryIssueEntryDTO> Handle(GetInventoryIssueEntryByIdQuery request, CancellationToken cancellationToken)
        {
            var inventoryIssueEntry = await _inventoryIssueEntryRepository.GetInventoryIssueEntryByIdAsync(request.InventoryIssueEntryId);
            if (inventoryIssueEntry is null)
            {
                throw new EntityNotFoundException(nameof(InventoryIssueEntry), request.InventoryIssueEntryId);
            }

            var inventoryIssueEntryDTO = _mapper.Map<InventoryIssueEntryDTO>(inventoryIssueEntry);

            var inventoryIssue = await _inventoryIssueRepository.GetInventoryIssueByIdAsync(inventoryIssueEntry.InventoryIssueId)
                              ?? throw new EntityNotFoundException($"Inventory Issue with Id {inventoryIssueEntry.InventoryIssueId} could not found");

            var employee = await _employeeRepository.GetEmployeeByIdAsync(inventoryIssue.EmployeeId)
                        ?? throw new EntityNotFoundException($"Employee with Id {inventoryIssue.EmployeeId} could not found");

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(inventoryIssue.WarehouseId)
                         ?? throw new EntityNotFoundException($"Warehouse Name with Id {inventoryIssue.WarehouseId} could not found");

            var material = await _materialRepository.GetMaterialByIdAsync(inventoryIssueEntry.MaterialId)
                        ?? throw new EntityNotFoundException($"Material with Id {inventoryIssueEntry.MaterialId} could not found");

            inventoryIssueEntryDTO.PersonId = employee.EmployeeId;
            inventoryIssueEntryDTO.WarehouseId = warehouse.WarehouseId;
            if (material.TryGetUnitOfMeasure(out var unitValue))
            {
                inventoryIssueEntryDTO.Unit = unitValue;
            }

            inventoryIssueEntryDTO.MapName(materialName: material.MaterialName,
                                           personName: employee.EmployeeName,
                                           warehouseName: warehouse.WarehouseName,
                                           lotNumber: inventoryIssueEntry.IssueLot.MaterialLotId,
                                           issueDate: inventoryIssue.IssueDate,
                                           unit: unitValue);

            return inventoryIssueEntryDTO;
        }
    }
}
