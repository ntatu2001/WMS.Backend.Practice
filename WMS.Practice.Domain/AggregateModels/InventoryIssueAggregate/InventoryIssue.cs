namespace WMS.Practice.Domain.AggregateModels.InventoryIssueAggregate
{
    public class InventoryIssue : Entity, IAggregateModel
    {
        public string InventoryIssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public IssueStatus IssueStatus { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<InventoryIssueEntry> Entries { get; set; }

        public InventoryIssue(string inventoryIssueId, DateTime issueDate, IssueStatus issueStatus, string customerId, string employeeId, string warehouseId)
        {
            InventoryIssueId = inventoryIssueId;
            IssueDate = issueDate;
            IssueStatus = issueStatus;
            CustomerId = customerId;
            EmployeeId = employeeId;
            WarehouseId = warehouseId;
        }

        public bool IsDone() => IssueStatus is IssueStatus.Done;
        public void RaiseInventoryLog(List<MaterialLot> materialLots, InventoryIssue inventoryIssue)
        {
            if (materialLots is null || materialLots.Count == 0)
                throw new Exception("MaterialLots is empty, cannot confirm issue.");

            foreach (var materialLot in materialLots)
            {
                double exisitingQuantity = materialLot.ExistingQuantity;
                foreach (var entry in inventoryIssue.Entries)
                {
                    if (entry.IssueLot.MaterialLotId == materialLot.LotNumber)
                    {
                        var changedQuantity = entry.IssueLot.RequestedQuantity;

                        AddDomainEvent(new InventoryLogAddedDomainEvent(transactionType: TransactionType.Issue,
                                                                        transactionDate: DateTime.Now.ToVietNamTime(),
                                                                        previousQuantity: exisitingQuantity,
                                                                        changedQuantity: changedQuantity,
                                                                        afterQuantity: exisitingQuantity - changedQuantity,
                                                                        note: "",
                                                                        lotNumber: materialLot.LotNumber,
                                                                        warehouseId: inventoryIssue.WarehouseId));

                        exisitingQuantity = exisitingQuantity - changedQuantity;

                    }
                }
            }
        }

        public void AddEntry(InventoryIssueEntry newEntry)
        {
            Entries ??= new List<InventoryIssueEntry>();
            Entries.Add(newEntry);
        }

        public InventoryIssueEntry? GetInventoryEntry(string entryId)
        {
            return Entries?.FirstOrDefault(x => x.InventoryIssueEntryId == entryId);
        }

        public void Update(string? customerId = null, string? employeeId = null, string? warehouseId = null, IssueStatus? issueStatus = null)
        {
            CustomerId = customerId ?? CustomerId;
            EmployeeId = employeeId ?? EmployeeId;
            WarehouseId = warehouseId ?? WarehouseId;
            IssueStatus = issueStatus ?? IssueStatus;
        }
    }
}
