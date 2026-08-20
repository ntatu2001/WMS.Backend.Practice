namespace WMS.Practice.Application.Exceptions
{
    [Serializable]
    public class LocationCapacityExceededException : Exception
    {
        public string? LocationId { get; set; }
        public double MaxVolume { get; set; }
        public double CurrentUsedVolume { get; set; }
        public double IncomingVolume { get; set; }
        public double ResultingRate { get; set; }

        public LocationCapacityExceededException(string locationId, double maxVolume, double currentUsedVolume,
            double incomingVolume, double resultingRate) :
            this($"Location {locationId} would reach {resultingRate:F2}% storage rate after adding this sublot, " +
                $"exceeding 100% capacity.")
        {
            LocationId = locationId;
            MaxVolume = maxVolume;
            CurrentUsedVolume = currentUsedVolume;
            IncomingVolume = incomingVolume;
            ResultingRate = resultingRate;
        }

        public LocationCapacityExceededException(string? message, Exception? innerException) : base(message, innerException)
        {

        }

        public LocationCapacityExceededException(string message) : base(message)
        {

        }

        protected LocationCapacityExceededException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {

        }
    }
}
