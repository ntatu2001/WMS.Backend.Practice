namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class CreateEmployeePropertyCommand : IRequest<bool>
    {
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string UnitOfMeasure { get; set; }
        public string EmployeeId { get; set; }

        public CreateEmployeePropertyCommand(string propertyName, string propertyValue, string unitOfMeasure, string employeeId)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            EmployeeId = employeeId;
        }
    }
}
