namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssueEntries
{
    public class DeleteInventoryIssueEntriesCommandHandler : IRequestHandler<DeleteInventoryIssueEntriesCommand, bool>
    {
        private readonly IInventoryIssueEntryRepository _inventoryIssueEntryRepository;

        public DeleteInventoryIssueEntriesCommandHandler(IInventoryIssueEntryRepository inventoryIssueEntryRepository)
        {
            _inventoryIssueEntryRepository = inventoryIssueEntryRepository;
        }

        public async Task<bool> Handle(DeleteInventoryIssueEntriesCommand request, CancellationToken cancellationToken)
        {
            var inventoryIssueEntry = await _inventoryIssueEntryRepository.GetInventoryIssueEntryByIdAsync(request.InventoryEntryId)
                                   ?? throw new EntityNotFoundException(nameof(InventoryIssueEntry), request.InventoryEntryId);

            if (inventoryIssueEntry.IssueLot.IsDone())
                throw new Exception($"Issue Lot of Entry {request.InventoryEntryId} is already done.");

            _inventoryIssueEntryRepository.Delete(inventoryIssueEntry);
            return await _inventoryIssueEntryRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
