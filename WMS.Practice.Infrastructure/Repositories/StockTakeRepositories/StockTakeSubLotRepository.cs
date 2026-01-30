namespace WMS.Practice.Infrastructure.Repositories.StockTakeRepositories
{
    public class StockTakeSubLotRepository : BaseRepository, IStockTakeSubLotRepository
    {
        public StockTakeSubLotRepository(WMSDbContext context) : base(context)
        {
        }

        public async Task<List<StockTakeSubLot>> GetStockTakeSubLotsByStockTakeId(string lotId)
        {
            return await _context.StockTakeSubLots
                                 .Where(stsl => stsl.StockTakeId == lotId)
                                 .ToListAsync();
        }
    }
}
