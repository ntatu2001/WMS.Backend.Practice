namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotPropertyByIdQuery : IRequest<MaterialLotPropertyDTO>
    {
        public string MaterialLotPropertyId { get; set; }

        public GetMaterialLotPropertyByIdQuery(string materialLotPropertyId)
        {
            MaterialLotPropertyId = materialLotPropertyId;
        }
    }
}
