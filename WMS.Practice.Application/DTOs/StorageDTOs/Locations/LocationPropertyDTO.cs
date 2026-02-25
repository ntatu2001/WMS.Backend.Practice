namespace WMS.Practice.Application.DTOs.StorageDTOs.Locations
{
    public class LocationPropertyDTO
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string LocationId { get; set; }

        public LocationPropertyDTO(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string locationId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
        }
    }
}
