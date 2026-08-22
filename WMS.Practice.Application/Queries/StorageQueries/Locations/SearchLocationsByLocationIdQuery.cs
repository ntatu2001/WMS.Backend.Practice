namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class SearchLocationsByLocationIdQuery : Query, IRequest<QueryResult<LocationDTO>>
    {
        public string? LocationId { get; set; }
        public string? WarehouseId { get; set; }

        public SearchLocationsByLocationIdQuery(string? locationId, string? warehouseId, int page, int itemsPerPage)
        {
            LocationId = locationId;
            WarehouseId = warehouseId;
            Page = page;
            ItemsPerPage = itemsPerPage;
        }
    }
}
