using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetMaterialClassByIdQuery : IRequest<MaterialClassDTO>
    {
        public string MaterialClassId { get; set; }

        public GetMaterialClassByIdQuery(string materialClassId)
        {
            MaterialClassId = materialClassId;
        }
    }
}
