namespace WMS.Practice.Application.Queries.OverviewQueries
{
    public class GetWarehouseInventoryMovementStatsQuery : IRequest<WarehouseInventoryMovementStatsDTO>
    {
        public TimeRangeOption TimeRange { get; set; }

        public GetWarehouseInventoryMovementStatsQuery(TimeRangeOption timeRange)
        {
            TimeRange = timeRange;
        }
    }
}
