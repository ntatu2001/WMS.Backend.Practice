namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class DeleteMaterialClassCommand : IRequest<bool>
    {
        public string MaterialClassId { get; set; }

        public DeleteMaterialClassCommand(string materialClassId)
        {
            MaterialClassId = materialClassId;
        }
    }
}
