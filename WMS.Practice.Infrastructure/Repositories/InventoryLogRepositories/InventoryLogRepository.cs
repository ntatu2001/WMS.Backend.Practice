namespace WMS.Practice.Infrastructure.Repositories.InventoryLogRepositories
{
    public class InventoryLogRepository : BaseRepository, IInventoryLogRepository
    {
        public InventoryLogRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(InventoryLog inventoryLog)
        {
            _context.InventoryLogs.Add(inventoryLog);
        }

        public async Task<List<InventoryLog>> GetAllInventoryLogs(TransactionType transactionType)
        {
            IQueryable<InventoryLog> query = _context.InventoryLogs;

            if (transactionType != default)
            {
                query = query.Where(x => x.TransactionType == transactionType);
            }

            if (transactionType is TransactionType.Issue)
            {
                query = query.Include(x => x.MaterialLot);
            }

            return await query.OrderByDescending(x => x.TransactionDate)
                              .ToListAsync();
        }

        public async Task<List<InventoryLog>> GetAllInventoryLogsByTime(TransactionType transactionType, DateTime dateTime)
        {
            return await _context.InventoryLogs
                                 .Where(x => x.TransactionType == transactionType && x.TransactionDate >= dateTime)
                                 .OrderByDescending(x => x.TransactionDate)
                                 .ToListAsync();
        }

        public async Task<List<InventoryLog>> GetInventoryLogByLotNumberAndStatus(string lotNumber, LotStatus status)
        {
            return await _context.InventoryLogs
                                 .Where(x => x.LotNumber == lotNumber && x.MaterialLot.LotStatus == status)
                                 .OrderByDescending(x => x.TransactionDate)
                                 .ToListAsync();
        }
    }
}
