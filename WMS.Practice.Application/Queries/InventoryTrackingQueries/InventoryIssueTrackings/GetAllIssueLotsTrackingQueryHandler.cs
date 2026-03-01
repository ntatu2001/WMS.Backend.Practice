namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryIssueTrackings
{
    public class GetAllIssueLotsTrackingQueryHandler : IRequestHandler<GetAllIssueLotsTrackingQuery, List<IssueLotsTrackingDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IIssueLotRepository _issueLotRepository;
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IIssueSubLotRepository _issueSubLotRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetAllIssueLotsTrackingQueryHandler(IWarehouseRepository warehouseRepository, IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, 
                                                   IIssueLotRepository issueLotRepository, IInventoryIssueRepository inventoryIssueRepository, 
                                                   IIssueSubLotRepository issueSubLotRepository, IMaterialRepository materialRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
            _issueLotRepository = issueLotRepository;
            _inventoryIssueRepository = inventoryIssueRepository;
            _issueSubLotRepository = issueSubLotRepository;
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<List<IssueLotsTrackingDTO>> Handle(GetAllIssueLotsTrackingQuery request, CancellationToken cancellationToken)
        {
            IQueryable<InventoryIssue> inventoryIssuesQuery = _inventoryIssueRepository.QueryAllInventoryIssues();

            if (!string.IsNullOrEmpty(request.CustomerId))
            {
                inventoryIssuesQuery = inventoryIssuesQuery.Where(s => s.CustomerId == request.CustomerId);
            }

            if (request.StartTime.HasValue && request.EndTime.HasValue)
            {
                inventoryIssuesQuery = inventoryIssuesQuery.Where(s =>s.IssueDate >= request.StartTime && s.IssueDate <= request.EndTime);
            }

            if (!string.IsNullOrEmpty(request.LotNumber))
            {
                inventoryIssuesQuery = inventoryIssuesQuery.Where(r => r.Entries.Any(e => e.IssueLot.MaterialLotId == request.LotNumber));
            }

            inventoryIssuesQuery = inventoryIssuesQuery.Where(r => r.Entries.Any());

            var inventoryIssues = await inventoryIssuesQuery.ToListAsync();
            if (inventoryIssues.Count == 0)
            {
                throw new EntityNotFoundException("Receipts Not Found");
            }

            var inventoryIssueTrackingDTOs = new List<IssueLotsTrackingDTO>();
            foreach (var inventoryIssue in inventoryIssuesQuery)
            {

                var (warehouseId, warehouseName) = await _warehouseRepository.GetWarehouseNameAndIdByIdAsync(inventoryIssue.WarehouseId)
                                                ?? throw new EntityNotFoundException($"Warehouse Name and Id could not found with {inventoryIssue.WarehouseId}");

                var (customerId, customerName) = await _customerRepository.GetCustomerNameAndIdByIdAsync(inventoryIssue.CustomerId)
                                              ?? throw new EntityNotFoundException($"Customer Id and Name could not found with {inventoryIssue.CustomerId}");

                var (employeeId, employeeName) = await _employeeRepository.GetEmployeeIdAndNameByIdAsync(inventoryIssue.EmployeeId)
                                              ?? throw new EntityNotFoundException($"Employee Id and Name could not found with {inventoryIssue.EmployeeId}");

                foreach (var entry in inventoryIssue.Entries)
                {
                    var material = await _materialRepository.GetMaterialByIdAsync(entry.MaterialId)
                                ?? throw new EntityNotFoundException($"Material with Id {entry.MaterialId} could not found");

                    if (material.TryGetUnitOfMeasure(out string unitOfMeasure) is false)
                        throw new EntityNotFoundException($"Unit Of Measure of Material with Id {entry.MaterialId} could not found");

                    var issueSublotDTOs = _mapper.Map<List<IssueSubLotDTO>>(entry.IssueLot.IssueSubLots);
                    var InventoryReceiptTrackingDTO = new IssueLotsTrackingDTO(warehouseID: warehouseId,
                                                                                 warehouseName: warehouseName,
                                                                                 personId: employeeId,
                                                                                 personName: employeeName,
                                                                                 customerId: customerId,
                                                                                 customerName: customerName,
                                                                                 issueDate: inventoryIssue.IssueDate,
                                                                                 lotNumber: entry.IssueLot.MaterialLotId,
                                                                                 requestedQuantity: entry.RequestedQuantity,
                                                                                 materialId: material.MaterialId,
                                                                                 materialName: material.MaterialName,
                                                                                 unitOfMeasure: unitOfMeasure,
                                                                                 lotStatus: entry.IssueLot.LotStatus,
                                                                                 issueSubLotDTOs: issueSublotDTOs);

                    inventoryIssueTrackingDTOs.Add(InventoryReceiptTrackingDTO);
                }
            }

            return inventoryIssueTrackingDTOs;
        }
    }
}
