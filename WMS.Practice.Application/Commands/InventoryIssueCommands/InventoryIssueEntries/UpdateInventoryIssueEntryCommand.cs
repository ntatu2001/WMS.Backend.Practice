namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssueEntries
{
    public class UpdateInventoryIssueEntryCommand : IRequest<bool>
    {
        public string InventoryIssueEntryId { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public double? RequestedQuantity { get; set; }
        public string? Note { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? IssueLotStatus { get; set; }
        public string InventoryIssueId { get; set; }

        public UpdateInventoryIssueEntryCommand(string inventoryIssueEntryId, string? purchaseOrderNumber, double? requestedQuantity, string? note, 
                                                string? materialId, string? materialName, string? issueLotStatus, string inventoryIssueId)
        {
            InventoryIssueEntryId = inventoryIssueEntryId;
            PurchaseOrderNumber = purchaseOrderNumber;
            RequestedQuantity = requestedQuantity;
            Note = note;
            MaterialId = materialId;
            MaterialName = materialName;
            IssueLotStatus = issueLotStatus;
            InventoryIssueId = inventoryIssueId;
        }
    }
}
