namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class DeleteEmployeePropertyCommand : IRequest<bool>
    {
        public string PropertyId { get; set; }

        public DeleteEmployeePropertyCommand(string propertyId)
        {
            PropertyId = propertyId;
        }
    }
}
