namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationsByWarehouseIdQuery : IRequest<List<LocationStatusInfoDTO>>
    {
        public string WarehouseId { get; set; }

        public GetLocationsByWarehouseIdQuery(string warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }
}
