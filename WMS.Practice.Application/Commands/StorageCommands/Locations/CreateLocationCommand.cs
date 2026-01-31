namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class CreateLocationCommand : IRequest<bool>
    {
        public string LocationId { get; set; }
        public string WarehouseId { get; set; }
        public List<LocationPropertyCommand> Properties { get; set; }

        public CreateLocationCommand(string locationId, string warehouseId, List<LocationPropertyCommand> properties)
        {
            LocationId = locationId;
            WarehouseId = warehouseId;
            Properties = properties;
        }
    }

    public class LocationPropertyCommand
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string UnitOfMeasure { get; set; }

        public LocationPropertyCommand(string propertyName, string propertyValue, string unitOfMeasure)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
        }
    }
}
