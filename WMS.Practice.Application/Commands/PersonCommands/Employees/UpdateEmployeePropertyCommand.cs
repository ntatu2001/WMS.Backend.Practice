namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class UpdateEmployeePropertyCommand : IRequest<bool>
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string UnitOfMeasure { get; set; }
        public string EmployeeId { get; set; }

        public UpdateEmployeePropertyCommand(string propertyId, string propertyName, string propertyValue, string unitOfMeasure, string employeeId)
        {
            PropertyId = propertyId;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            UnitOfMeasure = unitOfMeasure;
            EmployeeId = employeeId;
        }
    }
}
