namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class StockLocationHistoryRepository : BaseRepository, IStockLocationHistoryRepository
    {
        public StockLocationHistoryRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(StockLocationHistory stockLocationHistory)
        {
            _context.StockLocationHistories.Add(stockLocationHistory);
        }

        public async Task<List<StockLocationHistory>> GetByLocationIdAndTimeRangeAsync(string locationId, DateTime start, DateTime end)
        {
            return await _context.StockLocationHistories
                                 .Where(x => x.LocationId == locationId && x.EventDate >= start && x.EventDate <= end)
                                 .ToListAsync();
        }
    }
}
