namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (await _employeeRepository.ExistAsync(request.EmployeeId) is true)
            {
                throw new DuplicateRecordException("Employee is duplicated", nameof(request.EmployeeId));
            }

            var newEmployee = new Employee(employeeId: request.EmployeeId,
                                           employeeName: request.EmployeeName,
                                           employeeClassId: request.EmployeeClassId,
                                           properties: GetEmployeeProperties(request.Properties, request.EmployeeId));

            _employeeRepository.Create(newEmployee);
            return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private static IEnumerable<EmployeeProperty> GetEmployeeProperties(IList<EmployeePropertyCommand> commandProperties, string employeeId)
        {
            foreach (var commandProperty in commandProperties)
            {
                yield return new EmployeeProperty(propertyId: Guid.NewGuid().ToString(),
                                                  propertyName: commandProperty.PropertyName,
                                                  propertyValue: commandProperty.PropertyValue,
                                                  unitOfMeasure: commandProperty.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                  employeeId: employeeId);
            }
        }
    }
}
