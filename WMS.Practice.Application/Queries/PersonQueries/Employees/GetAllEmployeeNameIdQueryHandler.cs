using WMS.Practice.Application.DTOs.PersonDTOs.Employees;

namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetAllEmployeeNameIdQueryHandler : IRequestHandler<GetAllEmployeeNameIdQuery, IEnumerable<EmployeeNameIdDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeNameIdQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeNameIdDTO>> Handle(GetAllEmployeeNameIdQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllEmployeeNameIdAsync();

            return employees.Select(e => new EmployeeNameIdDTO(e.EmployeeId, e.EmployeeName));
        }
    }
}
