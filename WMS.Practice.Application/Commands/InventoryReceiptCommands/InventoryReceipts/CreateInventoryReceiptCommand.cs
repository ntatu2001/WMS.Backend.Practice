namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceipts
{
    public class CreateInventoryReceiptCommand : IRequest<string>
    {
        public DateTime? ReceiptDate { get; set; }
        public string SupplierId { get; set; }
        public string EmployeeId { get; set; }
        public string WarehouseId { get; set; }
        public List<CreateReceiptEntryCommand> Entries { get; set; }

        public CreateInventoryReceiptCommand(DateTime? receiptDate, string supplierId, string employeeId, string warehouseId, List<CreateReceiptEntryCommand> entries)
        {
            ReceiptDate = receiptDate;
            SupplierId = supplierId;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
            Entries = entries;
        }
    }
}
