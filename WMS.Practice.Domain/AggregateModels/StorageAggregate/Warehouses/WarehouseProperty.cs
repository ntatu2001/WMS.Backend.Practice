namespace WMS.Practice.Domain.AggregateModels.StorageAggregate
{
    public class WarehouseProperty : Entity, IAggregateModel
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public WarehouseProperty(string propertyId, string propertyName, string propertyValue, UnitOfMeasure unitOfMeasure, string warehouseId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            WarehouseId = warehouseId;
        }
    }
}
