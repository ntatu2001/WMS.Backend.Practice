namespace WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate
{
    public class InventoryIssueEntry : Entity, IAggregateModel
    {
        public string InventoryIssueEntryId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public double RequestedQuantity { get; set; }
        public string Note { get; set; }
        public string MaterialId { get; set; }
        public string MaterialName { get; set; }
        public string IssueLotId { get; set; }
        public IssueLot IssueLot { get; set; }
        public string InventoryIssueId { get; set; }
        public InventoryIssue InventoryIssue { get; set; }

        public InventoryIssueEntry(string inventoryIssueEntryId, string purchaseOrderNumber, double requestedQuantity, string note, string materialId, string materialName, string issueLotId, string inventoryIssueId)
        {
            InventoryIssueEntryId = inventoryIssueEntryId;
            PurchaseOrderNumber = purchaseOrderNumber;
            RequestedQuantity = requestedQuantity;
            Note = note;
            MaterialId = materialId;
            MaterialName = materialName;
            IssueLotId = issueLotId;
            InventoryIssueId = inventoryIssueId;
        }
    }
}
