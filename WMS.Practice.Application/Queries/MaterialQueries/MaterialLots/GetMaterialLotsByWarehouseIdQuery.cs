namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotsByWarehouseIdQuery : IRequest<IEnumerable<MaterialLotDTO>>
    {
        public string WarehouseId { get; set; }
        public GetMaterialLotsByWarehouseIdQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
