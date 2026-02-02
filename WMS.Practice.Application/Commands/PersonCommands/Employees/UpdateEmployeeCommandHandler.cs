namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId)
                                ?? throw new EntityNotFoundException("Employee could not found", nameof(request.EmployeeId));

            existingEmployee.UpdateEmployeeInfo(request.EmployeeName);
            foreach (var property in request.Properties)
            {
                if (existingEmployee.TryUpdateProperty(property.PropertyName, property.PropertyValue) is false)
                    throw new ArgumentException($"Property Name {property.PropertyName} is failed to update", nameof(property.PropertyName));
            }

            _employeeRepository.Update(existingEmployee);
            return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
