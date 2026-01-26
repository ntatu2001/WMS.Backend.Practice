namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class LocationProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string LocationId { get; set; }
        public Location Location { get; set; }

        public LocationProperty(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string locationId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
        }
    }
}
