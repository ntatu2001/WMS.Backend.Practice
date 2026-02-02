namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class DeleteEmployeePropertyCommandHandler : IRequestHandler<DeleteEmployeePropertyCommand, bool>
    {
        private readonly IEmployeePropertyRepository _employeePropertyRepository;
        public DeleteEmployeePropertyCommandHandler(IEmployeePropertyRepository employeePropertyRepository)
        {
            _employeePropertyRepository = employeePropertyRepository;
        }

        public async Task<bool> Handle(DeleteEmployeePropertyCommand request, CancellationToken cancellationToken)
        {
            var existingProperty = await _employeePropertyRepository.GetByIdAsync(request.PropertyId)
                                ?? throw new EntityNotFoundException("Employee Property did not exist", nameof(request.PropertyId));

            _employeePropertyRepository.Delete(existingProperty);
            return await _employeePropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
