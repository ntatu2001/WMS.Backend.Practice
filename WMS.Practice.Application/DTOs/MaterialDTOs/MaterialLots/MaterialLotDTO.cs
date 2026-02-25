namespace WMS.Practice.Application.DTOs.MaterialDTOs.MaterialLots
{
    public class MaterialLotDTO
    {
        public string? LotNumber { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LotStatus LotStatus { get; set; }
        public string? MaterialId { get; set; }
        public double? ExisitingQuantity { get; set; }
        public List<MaterialLotPropertyDTO> Properties { get; set; } = new List<MaterialLotPropertyDTO>();
        public List<MaterialSubLotDTO> SubLots { get; set; } = new List<MaterialSubLotDTO> { };
        public MaterialLotDTO()
        {
        }

        public MaterialLotDTO(string lotNumber, LotStatus lotStatus, string materialId, double exisitingQuantity, List<MaterialLotPropertyDTO> properties, List<MaterialSubLotDTO> subLots)
        {
            LotNumber = lotNumber;
            LotStatus = lotStatus;
            MaterialId = materialId;
            ExisitingQuantity = exisitingQuantity;
            Properties = properties;
            SubLots = subLots;
        }
    }
}
