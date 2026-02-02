namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class CreateEmployeeCommand : IRequest<bool>
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeClassId { get; set; }
        public List<EmployeePropertyCommand> Properties { get; set; }

        public CreateEmployeeCommand(string personId, string personName, string personClassId, List<EmployeePropertyCommand> properties)
        {
            EmployeeId = personId;
            EmployeeName = personName;
            EmployeeClassId = personClassId;
            Properties = properties;
        }
    }

    public class EmployeePropertyCommand
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string UnitOfMeasure { get; set; }

        public EmployeePropertyCommand(string propertyName, string propertyValue, string unitOfMeasure)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
        }
    }
}
