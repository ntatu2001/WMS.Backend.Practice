namespace WMS.Practice.Application.DTOs.StorageDTOs.Warehouses
{
    public class WarehouseNameIdDTO
    {
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        public WarehouseNameIdDTO(string warehouseId, string warehouseName)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
        }
    }
}
