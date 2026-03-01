namespace WMS.Practice.Application.Queries.InventoryIssueQueries.InventoryIssues
{
    public class GetInventoryIssueByIdQueryHandler : IRequestHandler<GetInventoryIssueByIdQuery, InventoryIssueDTO>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMediator _mediator;

        public GetInventoryIssueByIdQueryHandler(IInventoryIssueRepository inventoryIssueRepository, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IWarehouseRepository warehouseRepository, IMediator mediator)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
            _warehouseRepository = warehouseRepository;
            _mediator = mediator;
        }

        public async Task<InventoryIssueDTO> Handle(GetInventoryIssueByIdQuery request, CancellationToken cancellationToken)
        {
            var inventoryIssue = await _inventoryIssueRepository.GetInventoryIssueByIdAsync(request.InventoryIssueId)
                              ?? throw new EntityNotFoundException($"Inventory Issue with Id {request.InventoryIssueId} could not found");

            var employeeName = await _employeeRepository.GetEmployeeNameByIdAsync(inventoryIssue.EmployeeId)
                            ?? throw new EntityNotFoundException($"Employee Name with Id {inventoryIssue.EmployeeId} could not found");

            var customerName = await _customerRepository.GetCustomerNameByIdAsync(inventoryIssue.CustomerId)
                            ?? throw new EntityNotFoundException($"Customer Name with Id {inventoryIssue.CustomerId} could not found");

            var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(inventoryIssue.WarehouseId)
                             ?? throw new EntityNotFoundException($"Warehouse Name with Id {inventoryIssue.WarehouseId} could not found");

            var entries = new List<InventoryIssueEntryDTO>();
            foreach (var inventoryIssueEntry in inventoryIssue.Entries)
            {
                var inventoryIssueEntryDTO = await _mediator.Send(new GetInventoryIssueEntryByIdQuery(inventoryIssueEntry.InventoryIssueEntryId));
                entries.Add(inventoryIssueEntryDTO);
            }

            var inventoryIssueDTO = new InventoryIssueDTO(inventoryIssueId: inventoryIssue.InventoryIssueId,
                                                          issueDate: inventoryIssue.IssueDate,
                                                          issueStatus: inventoryIssue.IssueStatus,
                                                          customerName: customerName,
                                                          personName: employeeName,
                                                          warehouseName: warehouseName,
                                                          entries: entries);

            foreach (var entry in inventoryIssueDTO.Entries)
            {
                entry.MapName(inventoryIssueDTO.WarehouseName, inventoryIssueDTO.CustomerName, inventoryIssueDTO.PersonName);
            }

            return inventoryIssueDTO;
        }
    }
}
