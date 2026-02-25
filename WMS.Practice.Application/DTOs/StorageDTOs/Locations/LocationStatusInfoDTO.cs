namespace WMS.Practice.Application.DTOs.StorageDTOs.Locations
{
    public class LocationStatusInfoDTO
    {
        public string LocationId { get; set; }
        public string StorageStatus { get; set; }
        public List<LocationSubLotInfo> LotInfors { get; set; }

        public LocationStatusInfoDTO(string locationId, string storageStatus, List<LocationSubLotInfo> lotInfors)
        {
            LocationId = locationId;
            StorageStatus = storageStatus;
            LotInfors = lotInfors;
        }
    }
}
