namespace WMS.Practice.Domain.AggregateModels.IssueReceiptAggregate
{
    public class ReceiptLot : Entity, IAggregateModel
    {
        public string ReceiptLotId { get; private set; }
        public double ImportedQuantity { get; private set; }
        public LotStatus LotStatus { get; private set; }
        public Material Material { get; private set; }
        public string InventoryReceiptEntryId { get; private set; }
        public InventoryReceiptEntry InventoryReceiptEntry { get; private set; }
        public ReceiptLot(string receiptLotId, LotStatus lotStatus, double importedQuantity, string inventoryReceiptEntryId)
        {
            ReceiptLotId = receiptLotId;
            LotStatus = lotStatus;
            ImportedQuantity = importedQuantity;
            InventoryReceiptEntryId = inventoryReceiptEntryId;
        }
    }
}
