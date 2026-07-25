namespace WMS.Practice.Application.Services.Overviews
{
    public interface IOverviewService
    {
        (DateTime StartDate, DateTime EndDate) GetTimeRange(TimeRangeOption option);
    }
}
