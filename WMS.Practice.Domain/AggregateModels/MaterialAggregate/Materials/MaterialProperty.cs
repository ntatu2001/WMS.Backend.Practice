namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string MaterialId { get; set; }
        public Material Material { get; set; }
        public MaterialProperty(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string materialId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            MaterialId = materialId;
        }
    }
}
