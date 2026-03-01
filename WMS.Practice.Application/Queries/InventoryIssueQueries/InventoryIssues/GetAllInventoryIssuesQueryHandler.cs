namespace WMS.Practice.Application.Queries.InventoryIssueQueries.InventoryIssues
{
    public class GetAllInventoryIssuesQueryHandler : IRequestHandler<GetAllInventoryIssuesQuery, IEnumerable<InventoryIssueDTO>>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetAllInventoryIssuesQueryHandler(IInventoryIssueRepository inventoryIssueRepository, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryIssueDTO>> Handle(GetAllInventoryIssuesQuery request, CancellationToken cancellationToken)
        {
            var inventoryIssues = await _inventoryIssueRepository.GetAllInventoryIssuesAsync()
                               ?? throw new EntityNotFoundException($"Inventory Issues could not found");

            var inventoryIssueDTOs = new List<InventoryIssueDTO>();
            foreach (var inventoryIssue in inventoryIssues)
            {
                var inventoryIssueDTO = _mapper.Map<InventoryIssueDTO>(inventoryIssue);

                var employeeName = await _employeeRepository.GetEmployeeNameByIdAsync(inventoryIssue.EmployeeId)
                              ?? throw new EntityNotFoundException($"Employee Name with Id {inventoryIssue.EmployeeId} could not found");

                var customerName = await _customerRepository.GetCustomerNameByIdAsync(inventoryIssue.CustomerId)
                                ?? throw new EntityNotFoundException($"Customer Name with Id {inventoryIssue.CustomerId} could not found");

                var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(inventoryIssue.WarehouseId)
                                 ?? throw new EntityNotFoundException($"Warehouse Name with Id {inventoryIssue.WarehouseId} could not found");

                inventoryIssueDTO.MapName(customerName, employeeName, warehouseName);
                foreach (var entry in inventoryIssueDTO.Entries)
                {
                    entry.MapName(inventoryIssueDTO.WarehouseName, inventoryIssueDTO.CustomerName, inventoryIssueDTO.PersonName);
                }

                inventoryIssueDTOs.Add(inventoryIssueDTO);
            }

            return inventoryIssueDTOs;
        }
    }
}
