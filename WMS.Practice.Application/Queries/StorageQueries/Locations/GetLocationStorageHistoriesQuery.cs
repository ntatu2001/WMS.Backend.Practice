namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationStorageHistoriesQuery : TimeRangeQuery, IRequest<List<InventoryStorageTrackingDTO>>
    {
        public string LocationId { get; set; }

        public GetLocationStorageHistoriesQuery(string locationId, DateTime? startTime, DateTime? endTime)
        {
            LocationId = locationId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
