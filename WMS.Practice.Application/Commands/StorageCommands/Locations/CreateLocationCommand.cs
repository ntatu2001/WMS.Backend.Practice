namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class CreateLocationCommand : IRequest<bool>
    {
        public string LocationId { get; set; }
        public string WarehouseId { get; set; }
        public List<CreateLocationPropertyCommand> Properties { get; set; }

        public CreateLocationCommand(string locationId, string warehouseId, List<CreateLocationPropertyCommand> properties)
        {
            LocationId = locationId;
            WarehouseId = warehouseId;
            Properties = properties;
        }
    }
}
