namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetUnitByMaterialIdQuery : IRequest<string>
    {
        public string MaterialId { get; set; }

        public GetUnitByMaterialIdQuery(string materialId)
        {
            MaterialId = materialId;
        }
    }
}
