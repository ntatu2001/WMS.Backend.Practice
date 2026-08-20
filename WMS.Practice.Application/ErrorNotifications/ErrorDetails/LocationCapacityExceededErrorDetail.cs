namespace WMS.Practice.Application.ErrorNotifications.ErrorDetails
{
    public class LocationCapacityExceededErrorDetail
    {
        public string? LocationId { get; set; }
        public double MaxVolume { get; set; }
        public double CurrentUsedVolume { get; set; }
        public double IncomingVolume { get; set; }
        public double ResultingRate { get; set; }

        public LocationCapacityExceededErrorDetail(string? locationId, double maxVolume, double currentUsedVolume,
            double incomingVolume, double resultingRate)
        {
            LocationId = locationId;
            MaxVolume = maxVolume;
            CurrentUsedVolume = currentUsedVolume;
            IncomingVolume = incomingVolume;
            ResultingRate = resultingRate;
        }
    }
}
