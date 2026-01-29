namespace WMS.Practice.Infrastructure.Repositories.InventoryReceiptRepositories
{
    public class InventoryReceiptEntryRepository : BaseRepository, IInventoryReceiptEntryRepository
    {
        public InventoryReceiptEntryRepository(WMSDbContext context) : base(context)
        {
        }

        public void Delete(InventoryReceiptEntry inventoryReceiptEntry)
        {
            _context.InventoryReceiptEntries.Remove(inventoryReceiptEntry);
        }

        public async Task<List<InventoryReceiptEntry>> GetAllAsync()
        {
            return await _context.InventoryReceiptEntries
                                 .Include(x => x.ReceiptLot)
                                    .ThenInclude(x => x.Material)
                                 .Include(e => e.InventoryReceipt)
                                    .ThenInclude(e => e.Warehouse)
                                 .Include(e => e.InventoryReceipt)
                                    .ThenInclude(e => e.Employee)
                                 .ToListAsync();
        }

        public async Task<InventoryReceiptEntry?> GetById(string inventoryReceiptEntryId)
        {
            return await _context.InventoryReceiptEntries
                                 .Include(x => x.ReceiptLot)
                                    .ThenInclude(x => x.Material)
                                 .Include(e => e.InventoryReceipt)
                                    .ThenInclude(e => e.Warehouse)
                                 .Include(e => e.InventoryReceipt)
                                    .ThenInclude(e => e.Employee)
                                 .FirstOrDefaultAsync(x => x.InventoryReceiptEntryId == inventoryReceiptEntryId);
        }

        public async Task<InventoryReceiptEntry?> GetByReceiptLotId(string receiptLotId)
        {
            return await _context.InventoryReceiptEntries
                                 .Include(e => e.ReceiptLot)
                                    .ThenInclude(e => e.ReceiptSubLots)
                                 .FirstOrDefaultAsync(x => x.ReceiptLot.ReceiptLotId == receiptLotId);
        }
    }
}
