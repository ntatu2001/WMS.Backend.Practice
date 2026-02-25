namespace WMS.Practice.Application.DTOs.MaterialDTOs.Materials
{
    public class MaterialByWarehouseIdDTO
    {
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }


        public MaterialByWarehouseIdDTO(string materialId, string materialName)
        {
            MaterialId = materialId;
            MaterialName = materialName;
        }
    }
}
