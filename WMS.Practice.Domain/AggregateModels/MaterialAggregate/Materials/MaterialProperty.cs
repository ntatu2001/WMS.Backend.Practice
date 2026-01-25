namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string MaterialId { get; set; }
        public Material Material { get; set; }
        public MaterialProperty(string propertyId, string propertyName, UnitOfMeasure unitOfMeasure, string materialId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            UnitOfMeasure = unitOfMeasure;
            MaterialId = materialId;
        }
    }
}
