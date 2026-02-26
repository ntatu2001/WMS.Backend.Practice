using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetMaterialClassPropertyByIdQuery : IRequest<MaterialClassPropertyDTO>
    {
        public string MaterialClassPropertyId { get; set; }
        public GetMaterialClassPropertyByIdQuery(string materialClassPropertyId)
        {
            MaterialClassPropertyId = materialClassPropertyId;
        }
    }
}
