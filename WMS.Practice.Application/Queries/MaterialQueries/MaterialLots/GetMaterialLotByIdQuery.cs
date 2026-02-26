namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotByIdQuery : IRequest<MaterialLotDTO>
    {
        public string MaterialLotId { get; set; }

        public GetMaterialLotByIdQuery(string materialLotId)
        {
            MaterialLotId = materialLotId;
        }
    }
}
