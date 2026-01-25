namespace WMS.Practice.Domain.AggregateModels.MaterialAggregate
{
    public class MaterialLotProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string MaterialLotId { get; set; }
        public MaterialLot MaterialLot { get; set; }

        public MaterialLotProperty(string propertyId, string propertyName, UnitOfMeasure unitOfMeasure, string materialLotId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            UnitOfMeasure = unitOfMeasure;
            MaterialLotId = materialLotId;
        }
    }
}
