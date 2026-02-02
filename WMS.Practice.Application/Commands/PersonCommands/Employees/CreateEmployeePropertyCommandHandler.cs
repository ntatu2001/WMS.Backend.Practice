namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class CreateEmployeePropertyCommandHandler : IRequestHandler<CreateEmployeePropertyCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeePropertyRepository _propertyRepository;
        public CreateEmployeePropertyCommandHandler(IEmployeeRepository employeeRepository, IEmployeePropertyRepository propertyRepository)
        {
            _employeeRepository = employeeRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(CreateEmployeePropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _employeeRepository.ExistAsync(request.EmployeeId) is false)
            {
                throw new EntityNotFoundException("Entity could not found", nameof(request.EmployeeId));
            }

            var newEmployeeProperty = new EmployeeProperty(propertyId: Guid.NewGuid().ToString(),
                                                           propertyName: request.PropertyName,
                                                           propertyValue: request.PropertyValue,
                                                           unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                           employeeId: request.EmployeeId);

            _propertyRepository.Create(newEmployeeProperty);
            return await _propertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
