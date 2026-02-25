namespace WMS.Practice.Application.Queries.StorageQueries.Warehouses
{
    public class GetWarehouseIdByWarehouseNameQuery : IRequest<List<string>>
    {
        public string WarehouseName { get; set; }

        public GetWarehouseIdByWarehouseNameQuery(string warehouseName)
        {
            WarehouseName = warehouseName;
        }
    }
}
