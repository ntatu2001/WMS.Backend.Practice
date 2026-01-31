namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class DeleteLocationPropertyCommand : IRequest<bool>
    {
        public string LocationPropertyId { get; set; }
        public DeleteLocationPropertyCommand(string locationPropertyId)
        {
            LocationPropertyId = locationPropertyId;
        }
    }
}
