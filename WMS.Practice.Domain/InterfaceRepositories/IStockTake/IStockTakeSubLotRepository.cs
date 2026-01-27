namespace WMS.Practice.Domain.InterfaceRepositories.IStockTake
{
    public interface IStockTakeSubLotRepository : IRepository<StockTakeSubLot>
    {
        Task<List<StockTakeSubLot>> GetStockTakeSubLotsByStockTakeId(string lotId);
    }
}
