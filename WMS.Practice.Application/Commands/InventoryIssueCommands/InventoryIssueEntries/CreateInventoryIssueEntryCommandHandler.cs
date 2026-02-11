namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssueEntries
{
    public class CreateInventoryIssueEntryCommandHandler : IRequestHandler<CreateInventoryIssueEntryCommand, bool>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IIssueLotRepository _issueLotRepository;

        public CreateInventoryIssueEntryCommandHandler(IInventoryIssueRepository inventoryIssueRepository,
                                                       IMaterialRepository materialRepository, IIssueLotRepository issueLotRepository)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _materialRepository = materialRepository;
            _issueLotRepository = issueLotRepository;
        }

        public async Task<bool> Handle(CreateInventoryIssueEntryCommand request, CancellationToken cancellationToken)
        {
            var inventoryIssue = await _inventoryIssueRepository.GetByIdAsync(request.InventoryIssueId)
                              ?? throw new EntityNotFoundException(nameof(InventoryIssue), request.InventoryIssueId);

            if (inventoryIssue.IsDone())
                throw new Exception("The Issue has been Done");

            // Create a new Inventory Issue Entry based on Command Request
            var newEntry = await CreateInventoryIssueEntry(request);

            // Add new Entry into list of Entries in Inventory Issue
            inventoryIssue.AddEntry(newEntry);

            return await _inventoryIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        public async Task<InventoryIssueEntry> CreateInventoryIssueEntry(CreateInventoryIssueEntryCommand request)
        {
            if (await _issueLotRepository.ExistsAsync(request.IssueLotId) is true)
                throw new DuplicateRecordException(nameof(IssueLot), request.IssueLotId);

            var material = await _materialRepository.GetMaterialByIdAsync(request.MaterialId)
                        ?? throw new EntityNotFoundException(nameof(Material), request.MaterialId);

            var newEntry = new InventoryIssueEntry(inventoryIssueEntryId: Guid.NewGuid().ToString(),
                                                   purchaseOrderNumber: request.PurchaseOrderNumber,
                                                   requestedQuantity: request.RequestedQuantity ?? 0,
                                                   note: request.Note ?? "None",
                                                   materialId: request.MaterialId,
                                                   materialName: request.MaterialName ?? material.MaterialName,
                                                   issueLotId: request.IssueLotId,
                                                   inventoryIssueId: request.InventoryIssueId);

            newEntry.UpdateIssueLot(new IssueLot(issueLotId: newEntry.IssueLotId,
                                                 requestedQuantity: newEntry.RequestedQuantity,
                                                 lotStatus: LotStatus.Pending,
                                                 materialLotId: newEntry.PurchaseOrderNumber,    // PO cx chinh la LOT number
                                                 inventoryIssueEntryId: newEntry.InventoryIssueEntryId));

            return newEntry;
        }
    }
}
