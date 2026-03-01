using WMS.Practice.Application.DTOs.StockTakeDTOs;

namespace WMS.Practice.Application.Queries.StockTakeQueries
{
    public class GetAllStockTakeLotsQuery : IRequest<IEnumerable<StockTakeLotDTO>>
    {
        public GetAllStockTakeLotsQuery()
        {
        }

    }
}
