using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetAllMaterialClassNameIdQuery : IRequest<IEnumerable<MaterialClassNameIdDTO>>
    {
        public GetAllMaterialClassNameIdQuery() { }
    }
}
