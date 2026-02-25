namespace WMS.Practice.Application.DTOs.StorageDTOs.Locations
{
    public class LocationDTO
    {
        public string? LocationId { get; set; }
        public string? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public string? EquipmentName { get; set; } = "Ô chứa kệ hàng";
        public List<LocationPropertyDTO> LocationPropertyDTOs { get; set; } = new List<LocationPropertyDTO>();
        public List<MaterialSubLotDTO> MaterialSubLotDTOs { get; set; } = new List<MaterialSubLotDTO> { };

        public LocationDTO()
        {
        }

        public LocationDTO(string locationId, string warehouseName, List<MaterialSubLotDTO> materialSubLotDTOs) : this(locationId, warehouseName)
        {
            MaterialSubLotDTOs = materialSubLotDTOs;
        }

        public LocationDTO(string locationId, string warehouseName)
        {
            LocationId = locationId;
            WarehouseName = warehouseName;
            LocationPropertyDTOs = new List<LocationPropertyDTO>();
            MaterialSubLotDTOs = new List<MaterialSubLotDTO>();
        }
        
        public LocationDTO(string locationId, string warehouseName, string warehouseId)
        {
            LocationId = locationId;
            WarehouseName = warehouseName;
            WarehouseId = warehouseId;
            LocationPropertyDTOs = new List<LocationPropertyDTO>();
            MaterialSubLotDTOs = new List<MaterialSubLotDTO>();
        }

        public void UpdateWarehouseName(string warehouseName)
        {
            WarehouseName = warehouseName;
        }
    }
}
