namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialClassProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string MaterialClassId { get; set; }
        public MaterialClass MaterialClass { get; set; }
        public MaterialClassProperty(string propertyId, string propertyName, UnitOfMeasure unitOfMeasure, string materialClassId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            UnitOfMeasure = unitOfMeasure;
            MaterialClassId = materialClassId;
        }
    }
}
