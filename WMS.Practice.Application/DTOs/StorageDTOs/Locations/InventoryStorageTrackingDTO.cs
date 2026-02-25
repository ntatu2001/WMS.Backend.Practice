namespace WMS.Practice.Application.DTOs.StorageDTOs.Locations
{
    public class InventoryStorageTrackingDTO
    {
        public string MaterialName { get; set; }
        public string LotNumber { get; set; }
        public double? InboundQuantity { get; set; }
        public double? OutboundQuantity { get; set; }
        public double? AvailableQuantity { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? IssueDate { get; set; }

        public InventoryStorageTrackingDTO(string materialName, string lotNumber, double? inboundQuantity, double? outboundQuantity, double? availableQuantity, DateTime receiptDate, DateTime? issueDate)
        {
            MaterialName = materialName;
            LotNumber = lotNumber;
            InboundQuantity = inboundQuantity;
            OutboundQuantity = outboundQuantity;
            AvailableQuantity = availableQuantity;
            ReceiptDate = receiptDate;
            IssueDate = issueDate;
        }
    }
}
