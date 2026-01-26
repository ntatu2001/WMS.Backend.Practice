namespace WMS.Practice.Domain.AggregateModels.InventoryReceiptAggregate
{
    public class InventoryReceiptEntry : Entity, IAggregateModel
    {
        public string InventoryReceiptEntryId { get; private set; }
        public string PurchaseOrderNumber { get; private set; }
        public string MaterialId { get; private set; }
        public string MaterialName { get; private set; }
        public string Note { get; private set; }
        public double ImportedQuantity { get; private set; }
        public string LotNumber { get; private set; }
        public ReceiptLot ReceiptLot { get; private set; }
        public string InventoryReceiptId { get; private set; }
        public InventoryReceipt InventoryReceipt { get; private set; }

        public InventoryReceiptEntry(string inventoryReceiptEntryId, string purchaseOrderNumber, string materialId, string materialName, string note, double importedQuantity, string lotNumber, string inventoryReceiptId)
        {
            InventoryReceiptEntryId = inventoryReceiptEntryId;
            PurchaseOrderNumber = purchaseOrderNumber;
            MaterialId = materialId;
            MaterialName = materialName;
            Note = note;
            ImportedQuantity = importedQuantity;
            LotNumber = lotNumber;
            InventoryReceiptId = inventoryReceiptId;
        }
    }
}
