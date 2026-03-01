namespace WMS.Practice.Application.Queries.StockTakeQueries
{
    public class GetStockTakeByIdQuery : IRequest<StockTakeLotDTO>
    {
        public string StockTakeId { get; set; }
        public GetStockTakeByIdQuery(string stockTakeId)
        {
            StockTakeId = stockTakeId;
        }
    }
}
