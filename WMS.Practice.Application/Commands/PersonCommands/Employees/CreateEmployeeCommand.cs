namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class CreateEmployeeCommand : IRequest<bool>
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeClassId { get; set; }
        public List<CreateEmployeePropertyCommand> Properties { get; set; }

        public CreateEmployeeCommand(string personId, string personName, string personClassId, List<CreateEmployeePropertyCommand> properties)
        {
            EmployeeId = personId;
            EmployeeName = personName;
            EmployeeClassId = personClassId;
            Properties = properties;
        }
    }
}
