namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class UpdateInventoryReceiptEntryCommand : IRequest<bool>
    {
        public string InventoryReceiptEntryId { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public string? LotNumber { get; set; }
        public string? ReceiptLotStatus { get; set; }
        public double? ImportedQuantity { get; set; }
        public string? Note { get; set; }
        public string InventoryReceiptId { get; set; }

        public UpdateInventoryReceiptEntryCommand(string inventoryReceiptEntryId, string? materialId, string? materialName, string? purchaseOrderNumber, 
                                                  string? lotNumber, string? receiptLotStatus, double? importedQuantity, string? note, string inventoryReceiptId)
        {
            InventoryReceiptEntryId = inventoryReceiptEntryId;
            MaterialId = materialId;
            MaterialName = materialName;
            PurchaseOrderNumber = purchaseOrderNumber;
            LotNumber = lotNumber;
            ReceiptLotStatus = receiptLotStatus;
            ImportedQuantity = importedQuantity;
            Note = note;
            InventoryReceiptId = inventoryReceiptId;
        }
    }
}
