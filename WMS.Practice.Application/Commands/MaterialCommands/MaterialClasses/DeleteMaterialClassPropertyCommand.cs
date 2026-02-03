namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class DeleteMaterialClassPropertyCommand : IRequest<bool>
    {
        public string MaterialClassPropertyId { get; set; }

        public DeleteMaterialClassPropertyCommand(string materialClassPropertyId)
        {
            MaterialClassPropertyId = materialClassPropertyId;
        }
    }
}
