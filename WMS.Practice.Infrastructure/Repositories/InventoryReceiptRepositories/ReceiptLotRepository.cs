namespace WMS.Practice.Infrastructure.Repositories.InventoryReceiptRepositories
{
    public class ReceiptLotRepository : BaseRepository, IReceiptLotRepository
    {
        public ReceiptLotRepository(WMSDbContext context) : base(context)
        {
        }

        public async Task<List<ReceiptLot>> GetAllAsync()
        {
            return await _context.ReceiptLots
                                 .Include(rl => rl.Material)
                                    .ThenInclude(e => e.Properties)
                                 .Include(rl => rl.InventoryReceiptEntry)
                                    .ThenInclude(e => e.InventoryReceipt)
                                 .Include(rl => rl.ReceiptSubLots)
                                 .ToListAsync();
        }

        public async Task<ReceiptLot?> GetById(string Id)
        {
            return await _context.ReceiptLots
                                 .Include(rl => rl.Material)
                                    .ThenInclude(e => e.Properties)
                                 .Include(rl => rl.InventoryReceiptEntry)
                                    .ThenInclude(e => e.InventoryReceipt)
                                 .Include(rl => rl.ReceiptSubLots)
                                 .FirstOrDefaultAsync(rl => rl.ReceiptLotId == Id);
        }

        public async Task<ReceiptLot?> GetByIdAsync(string Id)
        {
            return await _context.ReceiptLots
                                 .Include(rl => rl.Material)
                                    .ThenInclude(e => e.Properties)
                                 .Include(rl => rl.InventoryReceiptEntry)
                                    .ThenInclude(e => e.InventoryReceipt)
                                 .Include(rl => rl.ReceiptSubLots)
                                 .FirstOrDefaultAsync(rl => rl.ReceiptLotId == Id);
        }

        public async Task<ReceiptLot?> GetReceiptByLotId(string Id)
        {
            return await _context.ReceiptLots
                                 .Include(rl => rl.Material)
                                    .ThenInclude(e => e.Properties)
                                 .Include(rl => rl.InventoryReceiptEntry)
                                    .ThenInclude(e => e.InventoryReceipt)
                                 .Include(rl => rl.ReceiptSubLots)
                                 .FirstOrDefaultAsync(rl => rl.ReceiptLotId == Id);
        }

        public async Task<List<ReceiptLot>> GetReceiptLotsAsPending()
        {
            return await _context.ReceiptLots
                                 .Include(rl => rl.Material)
                                    .ThenInclude(e => e.Properties)
                                 .Include(rl => rl.InventoryReceiptEntry)
                                    .ThenInclude(e => e.InventoryReceipt)
                                 .Include(rl => rl.ReceiptSubLots)
                                 .Where(rl => rl.LotStatus == LotStatus.Pending)
                                 .ToListAsync();
        }
    }
}
