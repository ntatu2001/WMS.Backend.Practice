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
    }
}
