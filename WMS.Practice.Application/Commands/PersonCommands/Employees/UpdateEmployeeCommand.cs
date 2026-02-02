namespace WMS.Practice.Application.Commands.EmployeeCommands.Employees
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        public string EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public List<EmployeePropertyUpdate> Properties { get; set; }

        public UpdateEmployeeCommand()
        {
        }

        public UpdateEmployeeCommand(string personId, string? personName, List<EmployeePropertyUpdate> properties)
        {
            EmployeeId = personId;
            EmployeeName = personName;
            Properties = properties;
        }
    }

    public class EmployeePropertyUpdate
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        public EmployeePropertyUpdate(string propertyName, string propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }
}
