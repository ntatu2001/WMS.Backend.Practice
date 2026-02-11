namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssueEntries
{
    public class UpdateInventoryIssueEntryCommandHandler : IRequestHandler<UpdateInventoryIssueEntryCommand, bool>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IIssueLoggingService _issueLoggingService;

        public UpdateInventoryIssueEntryCommandHandler(IInventoryIssueRepository inventoryIssueRepository, IMaterialRepository materialRepository, IIssueLoggingService issueLoggingService)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _materialRepository = materialRepository;
            _issueLoggingService = issueLoggingService;
        }

        public async Task<bool> Handle(UpdateInventoryIssueEntryCommand request, CancellationToken cancellationToken)
        {
            var inventoryIssue = await _inventoryIssueRepository.GetByIdAsync(request.InventoryIssueId)
                              ?? throw new EntityNotFoundException(nameof(InventoryIssue), request.InventoryIssueId);

            if (inventoryIssue.IsDone())
                throw new Exception("Inventory Issue has been saved");

            var entry = inventoryIssue.GetInventoryEntry(request.InventoryIssueEntryId)
                     ?? throw new EntityNotFoundException(nameof(InventoryIssueEntry), request.InventoryIssueEntryId);

            string materialId = request.MaterialId ?? entry.MaterialId;
            var material = await _materialRepository.GetMaterialByIdAsync(materialId)
                        ?? throw new EntityNotFoundException(nameof(Material), materialId);

            entry.Update(purchaseOrderNumber: request.PurchaseOrderNumber ?? entry.PurchaseOrderNumber,
                         materialId: request.MaterialId ?? material.MaterialId,
                         materialName: request.MaterialName ?? material.MaterialName,
                         note: request.Note ?? entry.Note,
                         requestedQuantity: request.RequestedQuantity ?? entry.RequestedQuantity,
                         lotStatus: request.IssueLotStatus?.ParseEnum<LotStatus>() ?? entry.IssueLot.LotStatus);

            await _issueLoggingService.UpdateInventoryLog(inventoryIssue);
            return await _inventoryIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
