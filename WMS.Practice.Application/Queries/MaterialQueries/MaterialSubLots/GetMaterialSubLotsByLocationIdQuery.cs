namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotsByLocationIdQuery : IRequest<IEnumerable<MaterialSubLotDTO>>
    {
        public string LocationId { get; set; }
        public GetMaterialSubLotsByLocationIdQuery(string locationId)
        {
            LocationId = locationId;
        }
    }
}
