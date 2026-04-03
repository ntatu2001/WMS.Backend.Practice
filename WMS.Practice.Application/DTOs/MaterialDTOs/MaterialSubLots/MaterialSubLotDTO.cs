namespace WMS.Practice.Application.DTOs.MaterialDTOs.MaterialSubLots
{
    public class MaterialSubLotDTO
    {
        public string MaterialSubLotId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LotStatus SubLotStatus { get; set; }
        public double ExistingQuantity { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string LocationId { get; set; }
        public string LotNumber { get; set; }

        // public List<IssueSubLots> issueSubLots { get; set; }

        public MaterialSubLotDTO(string materialSubLotId, LotStatus subLotStatus, double existingQuantity, UnitOfMeasure unitOfMeasure, string locationId, string lotNumber)
        {
            MaterialSubLotId = materialSubLotId;
            SubLotStatus = subLotStatus;
            ExistingQuantity = existingQuantity;
            UnitOfMeasure = unitOfMeasure;
            LocationId = locationId;
            LotNumber = lotNumber;
        }
    }
}
