namespace WMS.Practice.Application.Queries
{
    public class TimeRangeQuery : Query
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
