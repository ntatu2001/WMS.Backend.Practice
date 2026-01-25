namespace WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate
{
    public class InventoryIssue : Entity, IAggregateModel
    {
        public string InventoryIssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public IssueStatus Status { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<InventoryIssueEntry> Entries { get; set; }

        public InventoryIssue(string inventoryIssueId, DateTime issueDate, IssueStatus status, string customerId, string employeeId, string warehouseId)
        {
            InventoryIssueId = inventoryIssueId;
            IssueDate = issueDate;
            Status = status;
            CustomerId = customerId;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
        }
    }
}
