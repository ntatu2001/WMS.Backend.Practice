namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public string EmployeeId { get; set; }

        public DeleteEmployeeCommand(string employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
