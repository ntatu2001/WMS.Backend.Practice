namespace WMS.Practice.Domain.InterfaceRepositories.IStockTake
{
    public interface IStockTakeRepository : IRepository<StockTake>
    {
        Task<StockTake?> GetById(string Id);
        Task<List<StockTake>> GetAllStockTakeLotsAsync();
        Task<List<StockTake>> GetStockTakesWithTracking();
        Task<List<StockTake>> GetStockTakesByTimeRangeOption(DateTime start, DateTime end);
        IQueryable<StockTake?> QueryAllStockTakes();
        void Create(StockTake materialLotAdjustment);
        Task<bool> ExistsAsync(string stockTakeId);
    }
}
