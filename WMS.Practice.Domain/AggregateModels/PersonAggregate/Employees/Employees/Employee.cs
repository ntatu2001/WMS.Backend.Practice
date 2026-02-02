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
        public List<StockTake> StockTakes { get; private set; }

        public Employee(string employeeId, string employeeName, string employeeClassId, IEnumerable<EmployeeProperty>? properties = null)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeClassId = employeeClassId;
            Properties = properties?.ToList() ?? new List<EmployeeProperty>();
        }

        public void UpdateEmployeeInfo(string? employeeName)
        {
            EmployeeName = employeeName ?? EmployeeName;
        }

        public bool TryUpdateProperty(string? propertyName, string? propertyValue)
        {
            if (string.IsNullOrEmpty(propertyName)) 
                return false;

            var employeeProperty = Properties.FirstOrDefault(p => p.PropertyName == propertyName);
            if (employeeProperty is null)
                return false;

            employeeProperty.UpdatePropertyValue(propertyValue);
            return true;
        }
    }
}
