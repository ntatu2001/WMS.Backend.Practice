namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, QueryResult<EmployeeDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<QueryResult<EmployeeDTO>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeesQuery = _employeeRepository.QueryEmployees()
                                                      .OrderBy(e => e.EmployeeId);

            var totalItems = await employeesQuery.CountAsync(cancellationToken);

            var skip = (request.Page - 1) * request.ItemsPerPage;
            var pagedEmployees = await employeesQuery.Skip(skip).Take(request.ItemsPerPage).ToListAsync(cancellationToken);

            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(pagedEmployees);

            return new QueryResult<EmployeeDTO>(results: employeeDTOs, totalItems: totalItems);
        }
    }
}
