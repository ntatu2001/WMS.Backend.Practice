namespace WMS.Practice.Application.Commands.InventoryIssueCommands.IssueLots
{
    public class UpdateIssueLotStatusCommandHandler : IRequestHandler<UpdateIssueLotStatusCommand, bool>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IIssueLotRepository _issueLotRepository;
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMediator _mediator;

        public UpdateIssueLotStatusCommandHandler(IInventoryIssueRepository inventoryIssueRepository, IIssueLotRepository issueLotRepository, IMediator mediator, 
                                                  IMaterialLotRepository materialLotRepository, IMaterialSubLotRepository materialSubLotRepository)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _issueLotRepository = issueLotRepository;
            _mediator = mediator;
            _materialLotRepository = materialLotRepository;
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<bool> Handle(UpdateIssueLotStatusCommand request, CancellationToken cancellationToken)
        {
            var issueLot = await _issueLotRepository.GetIssueLotByIdAsync(request.IssueLotId)
                        ?? throw new EntityNotFoundException(nameof(ReceiptLot), request.IssueLotId);

            if (issueLot.IsDone() || issueLot.IsPending())
                throw new Exception($"Issue lot {request.IssueLotId} is already Done or Pending.");

            issueLot.Update(lotStatus: request.IssueLotStatus?.ParseEnum<LotStatus>());
            if (issueLot.IsDone())
            {
                // If Issue Lot is updated to DONE, Material SubLots corresponding to Issue Lot will be minus the Existing Quantity
                await UpdateMaterialSubLot(issueLot);
            }

            await _issueLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            // Request API UpdateMaterialLotQuantity to update Existing Quantity from Material SubLots to Material Lot
            await _mediator.Send(new UpdateMaterialLotQuantityCommand(issueLot.MaterialLotId));

            // After updating Status for Issue Lot, verify the overall status of Inventory Issue 
            // Update to DONE for Inventory Issue if all Entries are DONE
            var inventoryIssue = await _inventoryIssueRepository.GetByIdAsync(issueLot.InventoryIssueEntry.InventoryIssueId)
                              ?? throw new EntityNotFoundException(nameof(InventoryReceipt), issueLot.InventoryIssueEntry.InventoryIssueId);

            var overallIssueStatus = GetOverallInventoryIssueStatus(inventoryIssue);
            inventoryIssue.Update(issueStatus: overallIssueStatus);
            return await _inventoryIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private async Task UpdateMaterialSubLot(IssueLot issueLot)
        {
            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(issueLot.MaterialLotId)
                           ?? throw new EntityNotFoundException(nameof(MaterialLot), issueLot.MaterialLotId);

            if (issueLot.RequestedQuantity > materialLot.ExistingQuantity)
                throw new Exception($"Requested quantity ({issueLot.RequestedQuantity}) exceeds available quantity ({materialLot.ExistingQuantity}) for lot {issueLot.MaterialLotId}");

            foreach (var issueSubLot in issueLot.IssueSubLots)
            {
                var materialSubLot = await _materialSubLotRepository.GetMaterialSubLotByIdAsync(issueSubLot.IssueSubLotId)
                                  ?? throw new EntityNotFoundException(nameof(MaterialSubLot), issueSubLot.IssueSubLotId);

                if (issueSubLot.RequestedQuantity > materialSubLot.ExistingQuantity)
                    throw new Exception($"(1)Requested quantity ({issueSubLot.RequestedQuantity}) exceeds available quantity ({materialSubLot.ExistingQuantity}) for sublot {issueSubLot.IssueSubLotId}");

                materialSubLot.Export(issueSubLot.RequestedQuantity);
            }
        }

        private IssueStatus GetOverallInventoryIssueStatus(InventoryIssue inventoryIssue) => inventoryIssue.Entries.All(x => x.IsDone())
                                                                                           ? IssueStatus.Done
                                                                                           : IssueStatus.InProgress;
    }
}
