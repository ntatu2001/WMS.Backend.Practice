namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class CreateMaterialClassCommand : IRequest<bool>
    {
        public string MaterialClassId { get; set; }
        public string ClassName { get; set; }

        public CreateMaterialClassCommand(string materialClassId, string className)
        {
            MaterialClassId = materialClassId;
            ClassName = className;
        }
    }
}
