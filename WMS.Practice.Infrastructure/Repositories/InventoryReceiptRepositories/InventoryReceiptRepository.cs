namespace WMS.Practice.Infrastructure.Repositories.InventoryReceiptRepositories
{
    public class InventoryReceiptRepository : BaseRepository, IInventoryReceiptRepository
    {
        public InventoryReceiptRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(InventoryReceipt inventoryReceipt)
        {
            _context.InventoryReceipts.Add(inventoryReceipt);
        }

        public void Delete(InventoryReceipt inventoryReceipt)
        {
            _context.InventoryReceipts.Remove(inventoryReceipt);
        }

        public async Task<List<InventoryReceipt>> GetAllAsync()
        {
            return await _context.InventoryReceipts
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.ReceiptLot)
                                        .ThenInclude(e => e.ReceiptSubLots)
                                 .ToListAsync();
        }

        public async Task<InventoryReceipt?> GetByIdAsync(string inventoryReceiptId)
        {
            return await _context.InventoryReceipts
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.ReceiptLot)
                                        .ThenInclude(e => e.ReceiptSubLots)
                                 .FirstOrDefaultAsync(ir => ir.InventoryReceiptId == inventoryReceiptId);
        }

        public async Task<List<InventoryReceipt>> GetInventoryReceiptsByEntryIds(List<string> entryId)
        {
            return await _context.InventoryReceipts
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.ReceiptLot)
                                        .ThenInclude(e => e.ReceiptSubLots)
                                 .Where(ir => ir.Entries.Any(e => entryId.Contains(e.InventoryReceiptEntryId)))
                                 .ToListAsync();
        }

        public async Task<List<InventoryReceipt>> GetInventoryReceiptsByLocationId(string locationId)
        {
            return await _context.InventoryReceipts
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.ReceiptLot)
                                        .ThenInclude(e => e.ReceiptSubLots)
                                 .Where(ir => ir.Entries.Any(e => e.ReceiptLot.ReceiptSubLots.Any(x => x.LocationId == locationId)
                                                               && e.ReceiptLot.LotStatus == LotStatus.Done))
                                 .ToListAsync();
        }

        public async Task<List<InventoryReceipt>> GetInventoryReceiptsByTimeRangeOption(DateTime start, DateTime end)
        {
            return await _context.InventoryReceipts
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.ReceiptLot)
                                        .ThenInclude(e => e.ReceiptSubLots)
                                 .Where(ir => ir.ReceiptDate >= start && ir.ReceiptDate <= end)
                                 .ToListAsync();
        }
    }
}
