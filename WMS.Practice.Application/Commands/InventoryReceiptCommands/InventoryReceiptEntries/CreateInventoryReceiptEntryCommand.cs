namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceiptEntries
{
    public class CreateInventoryReceiptEntryCommand : IRequest<bool>
    {
        public string MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? PurchaseOrderNumber { get; set; }
        public double? ImportedQuantity { get; set; }
        public string? Note { get; set; }
        public string LotNumber { get; set; }
        public string InventoryReceiptId { get; set; }

        public CreateInventoryReceiptEntryCommand(string materialId, string? materialName, string? purchaseOrderNumber, double? importedQuantity, string? note, string lotNumber, string inventoryReceiptId)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            PurchaseOrderNumber = purchaseOrderNumber;
            ImportedQuantity = importedQuantity;
            Note = note;
            LotNumber = lotNumber;
            InventoryReceiptId = inventoryReceiptId;
        }
    }
}
