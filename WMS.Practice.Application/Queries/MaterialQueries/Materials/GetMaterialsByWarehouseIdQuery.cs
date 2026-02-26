namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialsByWarehouseIdQuery : IRequest<List<MaterialByWarehouseIdDTO>>
    {
        public string WarehouseId { get; set; }

        public GetMaterialsByWarehouseIdQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
