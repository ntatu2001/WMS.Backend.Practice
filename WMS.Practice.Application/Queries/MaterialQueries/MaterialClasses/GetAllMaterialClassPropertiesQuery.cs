using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetAllMaterialClassPropertiesQuery : IRequest<IEnumerable<MaterialClassPropertyDTO>>
    {
        public GetAllMaterialClassPropertiesQuery()
        {
        }
    }
}
