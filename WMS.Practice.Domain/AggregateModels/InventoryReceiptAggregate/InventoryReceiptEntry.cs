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

        public void UpdateReceiptLot(ReceiptLot receiptLot)
        {
            ReceiptLot = receiptLot;
        }

        public void Update(string? purchaseOrderNumber = null, string? materialId = null, string? materialName = null, string? note = null, 
                           double? importedQuantity = null, string? lotNumber = null, LotStatus? status = null)
        {
            UpdateIfNotNull(purchaseOrderNumber, e => PurchaseOrderNumber = e);
            UpdateIfNotNull(materialId, e => MaterialId = e);
            UpdateIfNotNull(materialName, e => MaterialName = e);
            UpdateIfNotNull(note, e => Note = e);
            UpdateIfNotNull(importedQuantity, e => ImportedQuantity = e);
            UpdateIfNotNull(lotNumber, e => LotNumber = e);

            ReceiptLot?.Update(importedQuantity: importedQuantity,
                               lotStatus: status);
        }

        public static void UpdateIfNotNull<T>(T? updateValue, Action<T> updateAction) where T : struct
        {
            if (updateValue.HasValue)
                updateAction(updateValue.Value);
        }

        public static void UpdateIfNotNull<T>(T? updateValue, Action<T> updateAction) where T : class
        {
            if (updateValue is not null)
                updateAction(updateValue);
        }
    }
}
