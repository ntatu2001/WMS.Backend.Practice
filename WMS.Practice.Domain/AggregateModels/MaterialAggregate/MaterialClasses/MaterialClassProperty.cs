namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialClassProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string MaterialClassId { get; set; }
        public MaterialClass MaterialClass { get; set; }
        public MaterialClassProperty(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string materialClassId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            MaterialClassId = materialClassId;
        }

        public void UpdatePropertyValue(string? propertyValue)
        {
            PropertyValue = propertyValue ?? PropertyValue;
        }

        public void Update(string? propertyName, string? propertyValue, UnitOfMeasure? unitOfMeasure)
        {
            PropertyName = propertyName ?? PropertyName;
            PropertyValue = propertyValue ?? PropertyValue;
            UnitOfMeasure = unitOfMeasure ?? UnitOfMeasure;
        }
    }
}
