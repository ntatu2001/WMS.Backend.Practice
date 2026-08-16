namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetLotNumbersByWarehouseIdQuery : IRequest<IEnumerable<string>>
    {
        public string WarehouseId { get; set; }
        public GetLotNumbersByWarehouseIdQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
