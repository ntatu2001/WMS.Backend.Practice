namespace WMS.Practice.Application.Commands.PersonCommands.Employees
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId)
                                ?? throw new EntityNotFoundException("Employee could not found", nameof(request.EmployeeId));

            _employeeRepository.Delete(existingEmployee);
            return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
