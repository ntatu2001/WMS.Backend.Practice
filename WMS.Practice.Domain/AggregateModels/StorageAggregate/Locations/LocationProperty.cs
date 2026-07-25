namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class LocationProperty : IAggregateModel
    {
        #region Properties
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string LocationId { get; set; }
        public Location Location { get; set; }

        #endregion

        #region Constructor

        public LocationProperty(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string locationId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
        }

        #endregion

        #region Update Methods

        public void UpdateLocationProperty(string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
        }

        #endregion
    }
}
