namespace WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses
{
    public class MaterialClassDTO
    {
        public string? MaterialClassId { get; set; }
        public string? ClassName { get; set; }
        public List<MaterialClassPropertyDTO> Properties { get; set; } = new List<MaterialClassPropertyDTO>();
        public List<MaterialDTO> Materials { get; set; } = new List<MaterialDTO> { };

        public MaterialClassDTO()
        {
        }

        public MaterialClassDTO(string materialClassId, string className, List<MaterialClassPropertyDTO> properties, List<MaterialDTO> materials)
        {
            MaterialClassId = materialClassId;
            ClassName = className;
            Properties = properties;
            Materials = materials;
        }
    }
}
