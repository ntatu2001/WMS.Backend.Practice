namespace WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses
{
    public class MaterialClassNameIdDTO
    {
        public string MaterialClassId { get; set; }
        public string MaterialClassName { get; set; }

        public MaterialClassNameIdDTO(string materialClassId, string materialClassName)
        {
            MaterialClassId = materialClassId;
            MaterialClassName = materialClassName;
        }
    }
}
