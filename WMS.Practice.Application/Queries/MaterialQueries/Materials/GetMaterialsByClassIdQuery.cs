namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialsByClassIdQuery : IRequest<IEnumerable<MaterialDTO>>
    {
        public string MaterialClassId { get; set; }

        public GetMaterialsByClassIdQuery(string materialClassId)
        {
            MaterialClassId = materialClassId;
        }
    }
}
