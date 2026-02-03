namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class UpdateMaterialClassCommand : IRequest<bool>
    {
        public string MaterialClassId { get; set; }
        public string? ClassName { get; set; }
        public List<UpdateMaterialClassPropertyCommand> Properties { get; set; } = new List<UpdateMaterialClassPropertyCommand>();

        public UpdateMaterialClassCommand()
        {
        }

        public UpdateMaterialClassCommand(string materialClassId, string className, List<UpdateMaterialClassPropertyCommand> properties)
        {
            MaterialClassId = materialClassId;
            ClassName = className;
            Properties = properties;
        }
    }
}
