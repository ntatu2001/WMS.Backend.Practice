namespace WMS.Practice.Domain.AggregateModels.InventoryReceiptAggregate
{
    public class ReceiptLot : Entity, IAggregateModel
    {
        public string ReceiptLotId { get; private set; }
        public double ImportedQuantity { get; private set; }
        public LotStatus LotStatus { get; private set; }
        public Material Material { get; private set; }
        public string InventoryReceiptEntryId { get; private set; }
        public InventoryReceiptEntry InventoryReceiptEntry { get; private set; }
        public List<ReceiptSubLot> ReceiptSubLots { get; private set; } 
        public ReceiptLot(string receiptLotId, LotStatus lotStatus, double importedQuantity, string inventoryReceiptEntryId)
        {
            ReceiptLotId = receiptLotId;
            LotStatus = lotStatus;
            ImportedQuantity = importedQuantity;
            InventoryReceiptEntryId = inventoryReceiptEntryId;
        }

        public bool IsDone() => LotStatus is LotStatus.Done;
        public bool IsNotDone() => LotStatus is not LotStatus.Done;
        public bool IsPending() => LotStatus is LotStatus.Pending;

        public void Update(double? importedQuantity = null, LotStatus? lotStatus = null, string? inventoryReceiptEntryId = null)
        {
            UpdateIfNotNull(importedQuantity, e => ImportedQuantity = e);
            UpdateIfNotNull(lotStatus, e => LotStatus = e);
            UpdateIfNotNull(inventoryReceiptEntryId, e => InventoryReceiptEntryId = e);
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

        public void AddSublot(ReceiptSubLot receiptSubLot)
        {
            ReceiptSubLots ??= new List<ReceiptSubLot>();
            ReceiptSubLots.Add(receiptSubLot);
        }
    }
}
