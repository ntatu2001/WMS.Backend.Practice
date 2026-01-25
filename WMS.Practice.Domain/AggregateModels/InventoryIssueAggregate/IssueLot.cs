namespace WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate
{
    public class IssueLot : Entity, IAggregateModel
    {
        public string IssueLotId { get; set; }
        public double RequestedQuantity { get; set; }
        public List<IssueSubLot> SubLots { get; set; } = new List<IssueSubLot>();
        public Material Material { get; set; }
        public LotStatus LotStatus { get; set; }
        public string MaterialLotId { get; set; }
        public MaterialLot MaterialLot { get; set; }
        public string InventoryIssueEntryId { get; set; }
        public InventoryIssueEntry InventoryIssueEntry { get; set; }
        public IssueLot(string issueLotId, double requestedQuantity, LotStatus lotStatus, string materialLotId, string inventoryIssueEntryId)
        {
            if (string.IsNullOrWhiteSpace(issueLotId))
                throw new ArgumentNullException(nameof(issueLotId));

            if (requestedQuantity < 0)
                throw new ArgumentException("Requested quantity cannot be negative.", nameof(requestedQuantity));

            IssueLotId = issueLotId;
            RequestedQuantity = requestedQuantity;
            LotStatus = lotStatus;
            MaterialLotId = materialLotId;
            InventoryIssueEntryId = inventoryIssueEntryId;
        }
    }
}
