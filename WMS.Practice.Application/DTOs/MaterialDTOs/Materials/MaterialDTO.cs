namespace WMS.Practice.Application.DTOs.MaterialDTOs.Materials
{
    public class MaterialDTO
    {
        public string? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? MaterialClassId { get; set; }
        public string? MaterialClassName { get; set; }
        public List<MaterialPropertyDTO> Properties { get; set; } = new List<MaterialPropertyDTO>();

        public MaterialDTO()
        {
        }

        public MaterialDTO(string materialId, string materialName, string materialClassId, string materialClassName, List<MaterialPropertyDTO> properties)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            MaterialClassId = materialClassId;
            MaterialClassName = materialClassName;
            Properties = properties;
        }
    }
}
