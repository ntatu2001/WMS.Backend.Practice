namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssueEntries
{
    public class CreateInventoryIssueEntryCommand : IRequest<bool>
    {
        public string PurchaseOrderNumber { get; set; }
        public double? RequestedQuantity { get; set; }
        public string? Note { get; set; }
        public string MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string InventoryIssueId { get; set; }
        public string IssueLotId { get; set; }

        public CreateInventoryIssueEntryCommand(string purchaseOrderNumber, double? requestedQuantity, string? note, string materialId, 
                                                string? materialName, string inventoryIssueId, string issueLotId)
        {
            PurchaseOrderNumber = purchaseOrderNumber;
            RequestedQuantity = requestedQuantity;
            Note = note;
            MaterialId = materialId;
            MaterialName = materialName;
            InventoryIssueId = inventoryIssueId;
            IssueLotId = issueLotId;
        }
    }
}
