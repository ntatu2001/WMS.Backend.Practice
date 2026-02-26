namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotByIdQuery : IRequest<MaterialSubLotDTO>
    {
        public string MaterialSubLotId { get; set; }

        public GetMaterialSubLotByIdQuery(string materialSubLotId)
        {
            MaterialSubLotId = materialSubLotId;
        }
    }
}
