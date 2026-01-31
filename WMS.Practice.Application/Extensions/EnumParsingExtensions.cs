namespace WMS.Practice.Application.Extensions
{
    public static class EnumParsingExtensions
    {
        public static T ParseEnum<T>(this string value) where T : struct, Enum
        {
            return Enum.TryParse(value, out T parseValue) ? parseValue : throw new ArgumentException($"Enum {value} could not be parsed");
        }
    }
}
