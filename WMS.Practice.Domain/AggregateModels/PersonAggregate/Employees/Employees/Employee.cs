namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class Employee : Entity, IAggregateModel
    {
        public string EmployeeId { get; private set; }
        public string EmployeeName { get; private set; }
        public string EmployeeClassId { get; private set; }
        public EmployeeClass EmployeeClass { get; private set; }
        public List<EmployeeProperty> Properties { get; private set; }
        public List<InventoryReceipt> InventoryReceipts { get; private set; }
        public List<InventoryIssue> InventoryIssues { get; private set; }

        public Employee(string employeeId, string employeeName, string employeeClassId)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeClassId = employeeClassId;
            Properties = new List<EmployeeProperty>();
        }
    }
}
