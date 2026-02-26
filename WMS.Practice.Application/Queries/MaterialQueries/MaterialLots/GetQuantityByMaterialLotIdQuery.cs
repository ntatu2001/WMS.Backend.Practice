namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetQuantityByMaterialLotIdQuery : IRequest<MaterialLotQuantityDTO>
    {
        public string MaterialLotId { get; set; }

        public GetQuantityByMaterialLotIdQuery(string materialLotId)
        {
            MaterialLotId = materialLotId;
        }
    }
}
