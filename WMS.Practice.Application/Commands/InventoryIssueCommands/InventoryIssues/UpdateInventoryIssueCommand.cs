namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssues
{
    public class UpdateInventoryIssueCommand : IRequest<bool>
    {
        public string InventoryIssueId { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string WarehouseId { get; set; }

        public UpdateInventoryIssueCommand(string inventoryIssueId, string customerId, string employeeId, string warehouseId)
        {
            InventoryIssueId = inventoryIssueId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
        }
    }
}
