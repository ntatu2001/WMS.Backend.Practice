namespace WMS.Practice.Application.Commands.MaterialCommands.Materials
{
    public class UpdateMaterialCommand : IRequest<bool>
    {
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialClassId { get; set; }
        public List<UpdateMaterialPropertyCommand> Properties { get; set; }

        public UpdateMaterialCommand(string materialId, string materialName, string materialClassId, List<UpdateMaterialPropertyCommand> properties)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialClassId = materialClassId;
            Properties = properties;
        }
    }
}
