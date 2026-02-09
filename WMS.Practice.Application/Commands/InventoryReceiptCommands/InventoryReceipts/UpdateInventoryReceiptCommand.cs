namespace WMS.Practice.Application.Commands.InventoryReceiptCommands.InventoryReceipts
{
    public class UpdateInventoryReceiptCommand : IRequest<bool>
    {
        public string InventoryReceiptId { get; set; }
        public string? SupplierId { get; set; }
        public string? EmployeeId { get; set; }
        public string? WarehouseId { get; set; }
        public string? Status { get; set; }

        public UpdateInventoryReceiptCommand(string inventoryReceiptId, string? supplierId, string? personId, string? warehouseId, string? status)
        {
            InventoryReceiptId = inventoryReceiptId;
            SupplierId = supplierId;
            EmployeeId = personId;
            WarehouseId = warehouseId;
            Status = status;
        }
    }
}
