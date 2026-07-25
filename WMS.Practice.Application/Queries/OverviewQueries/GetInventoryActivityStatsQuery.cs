namespace WMS.Practice.Application.Queries.OverviewQueries
{
    public class GetInventoryActivityStatsQuery : IRequest<InventoryActivityStatsDTO>
    {
        public TimeRangeOption TimeRange { get; set; }

        public GetInventoryActivityStatsQuery(TimeRangeOption timeRange)
        {
            TimeRange = timeRange;
        }
    }
}
