namespace WMS.Practice.Application.DTOs.InventoryIssueDTOs
{
    public class IssueLotDTO
    {
        public string? IssueLotId { get; set; }
        public double? RequestedQuantity { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LotStatus? IssueLotStatus { get; set; }
        public string? MaterialLotId { get; set; }
        public string? InventoryIssueEntryId { get; set; }
        public List<IssueSubLotDTO> IssueSublots { get; set; } = new List<IssueSubLotDTO>();

        public IssueLotDTO()
        {
        }
    }
}
