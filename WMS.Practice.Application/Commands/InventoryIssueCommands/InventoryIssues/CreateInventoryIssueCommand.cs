namespace WMS.Practice.Application.Commands.InventoryIssueCommands.InventoryIssues
{
    public class CreateInventoryIssueCommand : IRequest<string>
    {
        public DateTime? IssueDate { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public string WarehouseId { get; set; }

        public List<CreateIssueEntryCommand> Entries { get; set; }

        public CreateInventoryIssueCommand(DateTime? issueDate, string customerId, string employeeId, string warehouseId, List<CreateIssueEntryCommand> entries)
        {
            IssueDate = issueDate;
            CustomerId = customerId;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
            Entries = entries;
        }
    }

    public class CreateIssueEntryCommand
    {
        public string MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public double? RequestedQuantity { get; set; }
        public string? Note { get; set; }
        public string? Unit { get; set; }


        public CreateIssueEntryCommand(string materialId, string? materialName, string purchaseOrderNumber, double? requestedQuantity, string? note, string? unit)
        {
            MaterialId = materialId;
            MaterialName = materialName;
            PurchaseOrderNumber = purchaseOrderNumber;
            RequestedQuantity = requestedQuantity;
            Note = note;
            Unit = unit;
        }
    }
}
