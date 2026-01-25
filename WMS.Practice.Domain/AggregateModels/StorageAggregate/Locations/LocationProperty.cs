namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class LocationProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string LocationId { get; set; }
        public Location Location { get; set; }

        public LocationProperty(string propertyId, string propertyName, UnitOfMeasure unitOfMeasure, string locationId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
        }
    }
}
