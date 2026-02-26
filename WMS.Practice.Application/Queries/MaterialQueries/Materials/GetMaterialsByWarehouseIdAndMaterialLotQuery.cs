namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialsByWarehouseIdAndMaterialLotQuery : IRequest<IEnumerable<MaterialByWarehouseIdDTO>>
    {
        public string WarehouseId { get; set; }

        public GetMaterialsByWarehouseIdAndMaterialLotQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
