namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetLotNumbersByMaterialIdQuery : IRequest<IEnumerable<string>>
    {
        public string MaterialId { get; set; }
        public GetLotNumbersByMaterialIdQuery(string materialId)
        {
            MaterialId = materialId;
        }
    }
}
