namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssues
{
    public class CreateInventoryIssueCommandHandler : IRequestHandler<CreateInventoryIssueCommand, string>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IIssueLotRepository _issueLotRepository;

        public CreateInventoryIssueCommandHandler(IInventoryIssueRepository inventoryIssueRepository, IEmployeeRepository employeeRepository, 
                                                  ICustomerRepository customerRepository, IWarehouseRepository warehouseRepository, 
                                                  IMaterialRepository materialRepository, IIssueLotRepository issueLotRepository)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
            _warehouseRepository = warehouseRepository;
            _materialRepository = materialRepository;
            _issueLotRepository = issueLotRepository;
        }

        public async Task<string> Handle(CreateInventoryIssueCommand request, CancellationToken cancellationToken)
        {
            // Check the existence of Customer
            if (await _customerRepository.ExistsAsync(request.CustomerId) is false)
                throw new EntityNotFoundException(nameof(Customer), request.CustomerId);

            // Check the existence of Employee
            if (await _employeeRepository.ExistAsync(request.EmployeeId) is false)
                throw new EntityNotFoundException(nameof(Employee), request.EmployeeId);

            // Check the existence of Warehouse
            if (await _warehouseRepository.ExistsAsync(request.WarehouseId) is false)
                throw new EntityNotFoundException(nameof(Warehouse), request.WarehouseId);

            // Create a new Inventory Isseu from Command Request
            var newInventoryIssue = CreateNewInventoryIssue(request);
            foreach (var entry in request.Entries)
            {
                // For each entry in Request, create a new Inventory Issue Entry and add it into Inventory Issue
                var newEntry = await CreateInventoryIssueEntry(entry, newInventoryIssue.InventoryIssueId);
                newInventoryIssue.AddEntry(newEntry);
            }

            // Create a new Inventory Issue in MSSQL and return InventoryIssueId
            _inventoryIssueRepository.Create(newInventoryIssue);
            await _inventoryIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return newInventoryIssue.InventoryIssueId;

        }

        private InventoryIssue CreateNewInventoryIssue(CreateInventoryIssueCommand request) => new InventoryIssue(inventoryIssueId: Guid.NewGuid().ToString(),
                                                                                                                  issueDate: request.IssueDate ?? DateTime.Now.ToVietNamTime(),
                                                                                                                  issueStatus: IssueStatus.Pending,
                                                                                                                  customerId: request.CustomerId,
                                                                                                                  employeeId: request.EmployeeId,
                                                                                                                  warehouseId: request.WarehouseId);
        private async Task<InventoryIssueEntry> CreateInventoryIssueEntry(CreateIssueEntryCommand request, string inventoryIssueId)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(request.MaterialId)
                        ?? throw new EntityNotFoundException(nameof(Material), request.MaterialId);

            if (await _issueLotRepository.ExistsAsync(request.PurchaseOrderNumber) is true)
                throw new DuplicateRecordException(nameof(ReceiptLot), request.PurchaseOrderNumber);

            var newEntry = new InventoryIssueEntry(inventoryIssueEntryId: Guid.NewGuid().ToString(),
                                                   purchaseOrderNumber: request.PurchaseOrderNumber,
                                                   materialId: request.MaterialId,
                                                   materialName: request.MaterialName ?? material.MaterialName,
                                                   note: request.Note ?? "None",
                                                   requestedQuantity: request.RequestedQuantity ?? 0,
                                                   inventoryIssueId: inventoryIssueId,
                                                   issueLotId: Guid.NewGuid().ToString());

            newEntry.UpdateIssueLot(new IssueLot(issueLotId: newEntry.IssueLotId,
                                                 requestedQuantity: newEntry.RequestedQuantity,
                                                 lotStatus: LotStatus.Pending,
                                                 inventoryIssueEntryId: newEntry.InventoryIssueEntryId,
                                                 materialLotId: newEntry.PurchaseOrderNumber));

            return newEntry;
        }
    }
}
