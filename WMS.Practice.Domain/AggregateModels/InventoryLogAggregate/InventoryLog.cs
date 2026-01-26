namespace WMS.Practice.Domain.AggregateModels.InventoryLogAggregate
{
    public class InventoryLog : Entity, IAggregateModel
    {
        public string InventoryLogId { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public double PreviousQuantity { get; private set; }
        public double ChangedQuantity { get; private set; }
        public double AfterQuantity { get; private set; }
        public string Note { get; private set; }

        public string LotNumber { get; private set; }
        public MaterialLot MaterialLot { get; private set; }

        public string WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }

        public InventoryLog(string inventoryLogId, DateTime transactionDate, TransactionType transactionType, double previousQuantity, double changedQuantity, double afterQuantity, string lotNumber, string warehouseId, string note = "")
        {
            InventoryLogId = inventoryLogId;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            PreviousQuantity = previousQuantity;
            ChangedQuantity = changedQuantity;
            AfterQuantity = afterQuantity;
            LotNumber = lotNumber;
            WarehouseId = warehouseId;
            Note = note;
        }
    }
}
