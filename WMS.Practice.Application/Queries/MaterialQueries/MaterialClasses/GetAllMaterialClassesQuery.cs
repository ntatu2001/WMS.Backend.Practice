using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetAllMaterialClassesQuery : IRequest<IEnumerable<MaterialClassDTO>>
    {
        public GetAllMaterialClassesQuery()
        {

        }
    }
}
