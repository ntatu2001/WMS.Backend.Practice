namespace WMS.Practice.Application.Services.Overviews
{
    public class OverviewService : IOverviewService
    {
        public (DateTime StartDate, DateTime EndDate) GetTimeRange(TimeRangeOption option)
        {
            var now = DateTime.UtcNow.ToVietNamTime();

            DateTime startDate;
            DateTime endDate;

            if (option == TimeRangeOption.Today)
            {
                startDate = now.Date;
                endDate = startDate.AddDays(1).AddTicks(-1);
            }
            else if (option == TimeRangeOption.ThisWeek)
            {
                int dayOfWeek = (int)now.DayOfWeek;
                int daysToSubtract = dayOfWeek == 0 ? 6 : dayOfWeek - 1;

                startDate = now.Date.AddDays(-daysToSubtract);
                endDate = startDate.AddDays(7).AddTicks(-1);
            }
            else if (option == TimeRangeOption.ThisMonth)
            {
                startDate = new DateTime(now.Year, now.Month, 1);
                endDate = startDate.AddMonths(1).AddTicks(-1);
            }
            else if (option == TimeRangeOption.ThisYear)
            {
                startDate = new DateTime(now.Year, 1, 1);
                endDate = startDate.AddYears(1).AddTicks(-1);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(option), "Unknown time range option");
            }

            return (startDate, endDate);
        }
    }
}
