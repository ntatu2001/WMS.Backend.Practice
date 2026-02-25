namespace WMS.Practice.Application.Services.InventoryLogs.InventoryIssues
{
    public class IssueLoggingService : IIssueLoggingService
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        public IssueLoggingService(IMaterialLotRepository materialLotRepository, IMaterialSubLotRepository materialSubLotRepository)
        {
            _materialLotRepository = materialLotRepository;
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task UpdateInventoryLog(InventoryIssue inventoryIssue)
        {
            var materialLots = new HashSet<MaterialLot>();
            var finalStatus = DetermineFinalIssueStatus(inventoryIssue);

            foreach (var entry in inventoryIssue.Entries)
            {
                var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(entry.IssueLot.MaterialLotId)
                               ?? throw new EntityNotFoundException(nameof(MaterialLot), entry.IssueLot.MaterialLotId);

                if (entry.IssueLot.RequestedQuantity > materialLot.ExistingQuantity)
                    throw new InvalidOperationException($"Requested quantity ({entry.IssueLot.RequestedQuantity}) exceeds existing quantity ({materialLot.ExistingQuantity}) for lot {materialLot.LotNumber}.");

                await ValidateSubLotsAsync(entry.IssueLot);

                materialLots.Add(materialLot);
            }

            if (materialLots.Count == 0)
                throw new InvalidOperationException("Material lots collection is empty. Cannot confirm inventory issue.");

            inventoryIssue.RaiseInventoryLog(materialLots.ToList(), inventoryIssue);
            inventoryIssue.IssueStatus = finalStatus;
        }


        private static IssueStatus DetermineFinalIssueStatus(InventoryIssue issue)
        {
            var hasPendingLots = issue.Entries.Any(e => e.IssueLot.LotStatus != LotStatus.Done);
            if (!hasPendingLots)
                return IssueStatus.Done;

            return issue.IssueStatus == IssueStatus.Done ? IssueStatus.InProgress : issue.IssueStatus;
        }

        private async Task ValidateSubLotsAsync(IssueLot issueLot)
        {
            foreach (var subLot in issueLot.IssueSubLots)
            {
                var materialSubLot = await _materialSubLotRepository.GetByIdAsync(subLot.MaterialSubLotId)
                                  ?? throw new EntityNotFoundException(nameof(MaterialSubLot), subLot.MaterialSubLotId);

                if (subLot.RequestedQuantity > materialSubLot.ExistingQuantity)
                    throw new InvalidOperationException($"Requested quantity ({subLot.RequestedQuantity}) exceeds existing quantity ({materialSubLot.ExistingQuantity}) for sub-lot {materialSubLot.MaterialSubLotId}.");
            }
        }
    }
}
