using WMS.Practice.Application.DTOs.StorageDTOs.Locations;

namespace WMS.Practice.Application.DTOs.StorageDTOs.Warehouses
{
    public class WarehouseDTO
    {
        public string? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public List<WarehousePropertyDTO> Properties { get; set; } = new List<WarehousePropertyDTO>();
        public List<LocationDTO> Locations { get; set; } = new List<LocationDTO>();

        public WarehouseDTO()
        {
        }

        public WarehouseDTO(string warehouseId, string warehouseName, List<LocationDTO> locations, List<WarehousePropertyDTO> properties)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            Locations = locations;
            Properties = properties;
        }
    }
}
