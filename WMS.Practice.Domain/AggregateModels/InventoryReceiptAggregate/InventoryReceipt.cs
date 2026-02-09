namespace WMS.Practice.Domain.AggregateModels.InventoryReceiptAggregate
{
    public class InventoryReceipt : Entity, IAggregateModel
    {
        public string InventoryReceiptId { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public ReceiptStatus ReceiptStatus { get; private set; }
        public string SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }
        public string EmployeeId { get; private set; }
        public Employee Employee { get; private set; }
        public string WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }
        public List<InventoryReceiptEntry> Entries { get; set; }

        public InventoryReceipt(string inventoryReceiptId, ReceiptStatus receiptStatus, DateTime receiptDate, string supplierId, string employeeId, string warehouseId)
        {
            InventoryReceiptId = inventoryReceiptId;
            ReceiptStatus = receiptStatus;
            ReceiptDate = receiptDate;
            SupplierId = supplierId;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
            Entries = new List<InventoryReceiptEntry>();
        }

        public void UpdateReceiptStatus(ReceiptStatus receiptStatus)
        {
            ReceiptStatus = receiptStatus;
        }

        public void UpdateReceiptEntry(InventoryReceiptEntry entry)
        {
            if (Entries is null)
                Entries = new List<InventoryReceiptEntry>();

            Entries.Add(entry);
        }

        public bool IsDone() => ReceiptStatus is ReceiptStatus.Done;
        public bool IsNotDone() => ReceiptStatus is not ReceiptStatus.Done;

        public void Update(string? supplierId = null, string? employeeId = null, string? warehouseId = null, ReceiptStatus? receiptStatus = null)
        {
            SupplierId = supplierId ?? SupplierId;
            EmployeeId = employeeId ?? EmployeeId;
            WarehouseId = warehouseId ?? WarehouseId;
            ReceiptStatus = receiptStatus ?? ReceiptStatus;
        }

        public void RaiseInventoryLog(List<MaterialLot> materialLots, InventoryReceipt inventoryReceipt)
        {
            foreach (var materialLot in materialLots)
            {

                AddDomainEvent(new InventoryLogAddedDomainEvent(transactionType: TransactionType.Receipt,
                                                                transactionDate: DateTime.Now.ToVietNamTime(),
                                                                previousQuantity: 0,
                                                                changedQuantity: materialLot.ExistingQuantity,
                                                                afterQuantity: materialLot.ExistingQuantity,
                                                                note: "",
                                                                lotNumber: materialLot.LotNumber,
                                                                warehouseId: inventoryReceipt.WarehouseId));
            }

        }

        public void AddEntry(InventoryReceiptEntry newEntry)
        {
            if (Entries is null)
                Entries = new List<InventoryReceiptEntry>();

            Entries.Add(newEntry);
        }

        public InventoryReceiptEntry? FindEntry(string entryId)
        {
            return Entries?.FirstOrDefault(x => x.InventoryReceiptEntryId.Equals(entryId, StringComparison.OrdinalIgnoreCase));
        }
    }
}
