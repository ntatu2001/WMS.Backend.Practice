namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class SearchEmployeesByEmployeeIdQueryHandler : IRequestHandler<SearchEmployeesByEmployeeIdQuery, QueryResult<EmployeeDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public SearchEmployeesByEmployeeIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<QueryResult<EmployeeDTO>> Handle(SearchEmployeesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var employeesQuery = _employeeRepository.QueryEmployees();

            if (!string.IsNullOrWhiteSpace(request.EmployeeId))
            {
                employeesQuery = employeesQuery.Where(e => e.EmployeeId.Contains(request.EmployeeId));
            }

            if (!string.IsNullOrWhiteSpace(request.EmployeeClassId))
            {
                employeesQuery = employeesQuery.Where(e => e.EmployeeClassId == request.EmployeeClassId);
            }

            employeesQuery = employeesQuery.OrderBy(e => e.EmployeeId);

            var totalItems = await employeesQuery.CountAsync(cancellationToken);

            var skip = (request.Page - 1) * request.ItemsPerPage;
            var pagedEmployees = await employeesQuery.Skip(skip).Take(request.ItemsPerPage).ToListAsync(cancellationToken);

            var employeeDTOs = _mapper.Map<List<EmployeeDTO>>(pagedEmployees);

            return new QueryResult<EmployeeDTO>(results: employeeDTOs, totalItems: totalItems);
        }
    }
}
