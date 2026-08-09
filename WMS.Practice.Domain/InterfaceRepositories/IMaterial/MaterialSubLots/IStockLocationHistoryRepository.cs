namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IStockLocationHistoryRepository : IRepository<StockLocationHistory>
    {
        void Create(StockLocationHistory stockLocationHistory);
        Task<List<StockLocationHistory>> GetByLocationIdAndTimeRangeAsync(string locationId, DateTime start, DateTime end);
    }
}
