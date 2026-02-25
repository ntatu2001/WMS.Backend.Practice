namespace WMS.Practice.Application.DTOs.StorageDTOs.Locations
{
    public class LocationStorageInfoDTO
    {
        public string EquipmentName { get; set; } = "Ô kệ chứa hàng";
        public string WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Status { get; set; }
        public double StorageRate { get; set; }
        public List<LocationSubLotInfo> LotInfors { get; set; }
        public double UsableVolume { get; set; }
        public double MaxVolume { get; set; }

        public LocationStorageInfoDTO(string warehouseId, string warehouseName, string length, string width, string height, string status, double storageRate, List<LocationSubLotInfo> lotInfors, double usableVolume, double maxVolume)
        {
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            Length = length;
            Width = width;
            Height = height;
            Status = status;
            StorageRate = storageRate;
            LotInfors = lotInfors;
            UsableVolume = usableVolume;
            MaxVolume = maxVolume;
        }
    }

    public class LocationSubLotInfo
    {
        public string Lotnumber { get; set; }
        public double Quantity { get; set; }

        public LocationSubLotInfo(string lotnumber, double quantity)
        {
            Lotnumber = lotnumber;
            Quantity = quantity;
        }
    }
}
