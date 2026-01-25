namespace WMS.Practice.Domain.AggregateModels.IssueReceiptAggregate
{
    public class InventoryReceipt : Entity, IAggregateModel
    {
        public string InventoryReceiptId { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public string SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }
        public string EmployeeId { get; private set; }
        public Employee Employee { get; private set; }
        public string WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }
        public List<InventoryReceiptEntry> Entries { get; set; }

        public InventoryReceipt(string inventoryReceiptId, DateTime receiptDate, string supplierId, string employeeId, string warehouseId)
        {
            InventoryReceiptId = inventoryReceiptId;
            ReceiptDate = receiptDate;
            SupplierId = supplierId;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
            Entries = new List<InventoryReceiptEntry>();
        }
    }
}
