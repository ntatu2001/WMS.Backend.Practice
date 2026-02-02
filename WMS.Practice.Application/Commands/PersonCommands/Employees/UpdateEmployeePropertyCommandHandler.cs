namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class UpdateEmployeePropertyCommandHandler : IRequestHandler<UpdateEmployeePropertyCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeePropertyRepository _employeePropertyRepository;

        public UpdateEmployeePropertyCommandHandler(IEmployeeRepository employeeRepository, IEmployeePropertyRepository employeePropertyRepository)
        {
            _employeePropertyRepository = employeePropertyRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(UpdateEmployeePropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _employeeRepository.ExistAsync(request.EmployeeId) is false)
            {
                throw new EntityNotFoundException("Employee did not exist", nameof(request.EmployeeId));
            }

            var existingProperty = await _employeePropertyRepository.GetByIdAsync(request.PropertyId)
                                ?? throw new EntityNotFoundException("Employee Property did not exist", nameof(request.PropertyId));

            existingProperty.Update(propertyName: request.PropertyName,
                                    propertyValue: request.PropertyValue,
                                    unitOfMeasure: request.UnitOfMeasure?.ParseEnum<UnitOfMeasure>() ?? null);

            _employeePropertyRepository.Update(existingProperty);
            return await _employeePropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
