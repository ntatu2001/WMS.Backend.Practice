namespace WMS.Practice.Application.DTOs.InventoryIssueDTOs
{
    public class IssueSubLotDTO
    {
        public string IssueSublotId { get; set; }
        public double RequestedQuantity { get; set; }
        public MaterialSubLotDTO MaterialSublot { get; set; }
        public string IssueLotId { get; set; }
        public string? LocationId { get; set; }

        public IssueSubLotDTO(string issueSublotId, double requestedQuantity, MaterialSubLotDTO materialSublot, string issueLotId, string? locationId = null)
        {
            IssueSublotId = issueSublotId;
            RequestedQuantity = requestedQuantity;
            MaterialSublot = materialSublot;
            IssueLotId = issueLotId;
            LocationId = locationId;
        }
    }
}
