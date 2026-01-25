namespace WMS.Practice.Domain.AggregateModels.PersonAggregate
{
    public class EmployeeClass : Entity, IAggregateModel
    {
        public string EmployeeClassId { get; set; }
        public string EmployeeClassName { get; set; }
        public List<Employee> Employees { get; set; }
        public List<EmployeeClassProperty> Properties { get; set; }
        public EmployeeClass(string employeeClassId, string employeeClassName)
        {
            EmployeeClassId = employeeClassId;
            EmployeeClassName = employeeClassName;
            Properties = new List<EmployeeClassProperty>();
        }
    }
}
