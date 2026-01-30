namespace WMS.Practice.Domain.InterfaceRepositories.IStockTake
{
    public interface IStockTakeRepository : IRepository<StockTake>
    {
        Task<StockTake?> GetById(string Id);
        Task<List<StockTake>> GetAll();
        Task<List<StockTake>> GetStockTakesWithTracking();
        Task<List<StockTake>> GetStockTakesByTimeRangeOption(DateTime start, DateTime end);
        void Create(StockTake materialLotAdjustment);
    }
}
