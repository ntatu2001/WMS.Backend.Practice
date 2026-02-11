namespace WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate
{
    public class InventoryIssueEntry : Entity, IAggregateModel
    {
        public string InventoryIssueEntryId { get; private set; }
        public string PurchaseOrderNumber { get; private set; }
        public double RequestedQuantity { get; private set; }
        public string Note { get; private set; }
        public string MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public string IssueLotId { get; private set; }
        public IssueLot IssueLot { get; private set; }
        public string InventoryIssueId { get; private set; }
        public InventoryIssue InventoryIssue { get; private set; }

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

        public bool IsDone() => IssueLot is not null ? IssueLot.IsDone() : false;

        public void UpdateIssueLot(IssueLot issueLot)
        {
            IssueLot = issueLot;
        }

        public void Update(string? purchaseOrderNumber = null, string? materialId = null, string? materialName = null, 
                           string? note = null, double? requestedQuantity = null, LotStatus? lotStatus = null)
        {
            PurchaseOrderNumber = purchaseOrderNumber ?? PurchaseOrderNumber;
            MaterialId = materialId ?? MaterialId;
            MaterialName = materialName ?? MaterialName;
            Note = note ?? Note;
            RequestedQuantity = requestedQuantity ?? RequestedQuantity;

            IssueLot?.Update(requestedQuantity: requestedQuantity,
                             lotStatus: lotStatus);
        }
    }
}
