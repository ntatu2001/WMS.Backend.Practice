namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssues
{
    public class DeleteInventoryIssueCommandHandler : IRequestHandler<DeleteInventoryIssueCommand, bool>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;

        public DeleteInventoryIssueCommandHandler(IInventoryIssueRepository inventoryIssueRepository)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
        }

        public async Task<bool> Handle(DeleteInventoryIssueCommand request, CancellationToken cancellationToken)
        {
            var inventoryIssue = await _inventoryIssueRepository.GetByIdAsync(request.InventoryIssueId)
                              ?? throw new EntityNotFoundException(nameof(InventoryIssue), request.InventoryIssueId);

            if (inventoryIssue.IsDone())
                throw new Exception("The Issue has been saved");

            _inventoryIssueRepository.Delete(inventoryIssue);
            return await _inventoryIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
