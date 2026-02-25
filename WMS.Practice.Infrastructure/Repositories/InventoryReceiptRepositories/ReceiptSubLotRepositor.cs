namespace WMS.Practice.Infrastructure.Repositories.InventoryReceiptRepositories
{
    public class ReceiptSubLotRepositor : BaseRepository, IReceiptSubLotRepository
    {
        public ReceiptSubLotRepositor(WMSDbContext context) : base(context)
        {
        }

        public async Task<List<ReceiptSubLot>> GetAllAsync()
        {
            return await _context.ReceiptSubLots.ToListAsync();
        }

        public Task<ReceiptSubLot?> GetByIdAsync(string receiptSubLotId)
        {
            return _context.ReceiptSubLots
                           .FirstOrDefaultAsync(rsl => rsl.ReceiptSubLotId == receiptSubLotId);
        }

        public Task<List<ReceiptSubLot>> GetSubLotsByLotId(string lotId)
        {
            return _context.ReceiptSubLots
                           .Where(rsl => rsl.ReceiptLotId == lotId)
                           .ToListAsync();
        }

        public async Task<string?> GetMaterialNameByReceiptSubLotIdAsync(string receiptSubLotId)
        {
            return await _context.ReceiptSubLots.Where(s => s.ReceiptSubLotId == receiptSubLotId)
                                                .Select(s => s.ReceiptLot.Material.MaterialName)
                                                .FirstOrDefaultAsync();
        }


        public async Task<List<(DateTime ReceiptDate, ReceiptSubLot SubLot)>> GetReceiptSubLotsByLocationIdAndTimeRange(string locationId, DateTime start, DateTime end)
        {
            var data = await _context.InventoryReceipts.Where(receipt => receipt.ReceiptDate >= start && receipt.ReceiptDate <= end)
                                                        .SelectMany(receipt => receipt.Entries.Where(e => e.ReceiptLot.LotStatus == LotStatus.Done)
                                                            .SelectMany(e => e.ReceiptLot.ReceiptSubLots.Where(s => s.LocationId == locationId)
                                                                .Select(subLot => new { receipt.ReceiptDate, SubLot = subLot })))
                                                        .ToListAsync();

            return data.Select(x => (x.ReceiptDate, x.SubLot)).ToList();
        }
    }
}
