namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class CreateLocationPropertyCommand : IRequest<bool>
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string UnitOfMeasure { get; set; }
        public string LocationId { get; set; }

        public CreateLocationPropertyCommand(string propertyId, string propertyName, string propertyValue, string unitOfMeasure, string locationId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
        }
    }
}
