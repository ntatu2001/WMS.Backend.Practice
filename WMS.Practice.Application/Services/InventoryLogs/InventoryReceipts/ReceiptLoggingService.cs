namespace WMS.Practice.Application.Services.InventoryLogs.InventoryReceipts
{
    public class ReceiptLoggingService : IReceiptLoggingService
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        public ReceiptLoggingService(IMaterialLotRepository materialLotRepository)
        {
            _materialLotRepository = materialLotRepository;
        }

        public async Task UpdateInventoryLog(InventoryReceipt receipt)
        {
            var materialLots = await GetCompletedMaterialLotsAsync(receipt);
            var finalStatus = CalculateFinalReceiptStatus(receipt);

            // Raise Inventory Log for completion in status of Material Lots
            receipt.RaiseInventoryLog(materialLots, receipt);
            receipt.UpdateReceiptStatus(finalStatus);
        }

        private async Task<List<MaterialLot>> GetCompletedMaterialLotsAsync(InventoryReceipt receipt)
        {
            var materialLots = new List<MaterialLot>();
            foreach (var entry in receipt.Entries)
            {
                if (!entry.ReceiptLot.IsDone())
                    continue;

                // If Receipt Lot is DONE -> Create a new Material Lot for this Receipt Lot
                var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(entry.ReceiptLot.ReceiptLotId)
                               ?? throw new EntityNotFoundException(nameof(MaterialLot), entry.ReceiptLot.ReceiptLotId);

                materialLots.Add(materialLot);
            }

            return materialLots;
        }

        private static ReceiptStatus CalculateFinalReceiptStatus(InventoryReceipt receipt)
        {
            var hasPendingLots = receipt.Entries.Any(e => e.ReceiptLot.IsNotDone());
            return hasPendingLots ? ReceiptStatus.InProgress : ReceiptStatus.Done;
        }
    }
}
