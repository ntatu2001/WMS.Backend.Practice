namespace WMS.Practice.Infrastructure.Repositories.StockTakeRepositories
{
    public class StockTakeRepository : BaseRepository, IStockTakeRepository
    {
        public StockTakeRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(StockTake materialLotAdjustment)
        {
            _context.StockTakes.Add(materialLotAdjustment);
        }

        public async Task<List<StockTake>> GetAll()
        {
            return await _context.StockTakes
                                 .Include(x => x.SubLots)
                                 .OrderByDescending(x => x.AdjustmentDate)
                                 .ToListAsync();

        }

        public async Task<List<StockTake>> GetStockTakesWithTracking()
        {
            return await _context.StockTakes
                                 .AsTracking()
                                 .Include(x => x.SubLots)
                                 .Include(x => x.MaterialLot)
                                 .OrderByDescending(x => x.AdjustmentDate)
                                 .ToListAsync();
        }

        public async Task<StockTake?> GetById(string stockTakeId)
        {
            return await _context.StockTakes   
                                 .Include(x => x.SubLots)
                                 .FirstOrDefaultAsync(x => x.StockTakeId == stockTakeId);
        }

        public async Task<List<StockTake>> GetStockTakesByTimeRangeOption(DateTime start, DateTime end)
        {
            return await _context.StockTakes
                                 .Include(x => x.SubLots)
                                 .Where(x => x.AdjustmentDate >= start && x.AdjustmentDate <= end)
                                 .OrderByDescending(x => x.AdjustmentDate)
                                 .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string stockTakeId)
        {
            return await _context.StockTakes.AnyAsync(x => x.StockTakeId == stockTakeId);
        }
    }
}
