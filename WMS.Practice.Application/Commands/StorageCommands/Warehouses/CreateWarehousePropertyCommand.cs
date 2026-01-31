namespace WMS.Practice.Application.Commands.StorageCommands.Warehouses
{
    public class CreateWarehousePropertyCommand : IRequest<bool>
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string UnitOfMeasure { get; set; }
        public string WarehouseId { get; set; }

        public CreateWarehousePropertyCommand(string propertyId, string propertyName, string propertyValue, string unitOfMeasure, string warehouseId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            WarehouseId = warehouseId;
        }
    }
}
