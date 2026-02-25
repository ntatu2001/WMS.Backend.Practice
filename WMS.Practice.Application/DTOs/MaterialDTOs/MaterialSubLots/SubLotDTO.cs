namespace WMS.Practice.Application.DTOs.MaterialDTOs.MaterialSubLots
{
    public class SubLotDTO
    {
        public string MaterialSubLotId { get; set; }
        public double ExistingQuality { get; set; }
        public double RealTimeQuality { get; set; }
        public string LocationId { get; set; }
        public string LotNumber { get; set; }
        public string Note { get; set; }

        public SubLotDTO(string materialSubLotId, double existingQuality, double realTimeQuality, string locationId, string lotNumber, string note)
        {
            MaterialSubLotId = materialSubLotId;
            ExistingQuality = existingQuality;
            RealTimeQuality = realTimeQuality;
            LocationId = locationId;
            LotNumber = lotNumber;
            Note = note;
        }
    }
}
