namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDTO>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync()
                         ?? throw new EntityNotFoundException("Employees could not found");

            var employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
            return employeeDTOs;
        }
    }
}
