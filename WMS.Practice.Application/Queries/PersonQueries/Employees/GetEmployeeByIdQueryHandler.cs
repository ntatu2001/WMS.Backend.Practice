namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId)
                                ?? throw new EntityNotFoundException($"Employee {request.EmployeeId} could not found", nameof(request.EmployeeId));

            var employeeDTO = _mapper.Map<EmployeeDTO>(existingEmployee);
            return employeeDTO;
        }
    }
}
