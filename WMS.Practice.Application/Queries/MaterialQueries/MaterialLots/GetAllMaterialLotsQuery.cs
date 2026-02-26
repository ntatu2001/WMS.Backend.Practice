using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialLots;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetAllMaterialLotsQuery : IRequest<IEnumerable<MaterialLotDTO>>
    {
        public GetAllMaterialLotsQuery() { }

    }
}
