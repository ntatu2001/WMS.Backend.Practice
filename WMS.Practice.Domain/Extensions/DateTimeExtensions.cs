namespace WMS.Practice.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToVietNamTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
        }
    }
}
