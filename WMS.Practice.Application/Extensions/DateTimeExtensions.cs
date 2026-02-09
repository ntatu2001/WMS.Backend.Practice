namespace WMS.Practice.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToVietNamTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        }
    }
}
