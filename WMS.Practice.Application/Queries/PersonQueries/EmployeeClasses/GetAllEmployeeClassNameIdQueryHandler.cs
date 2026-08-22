using WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses;

namespace WMS.Practice.Application.Queries.PersonQueries.EmployeeClasses
{
    public class GetAllEmployeeClassNameIdQueryHandler : IRequestHandler<GetAllEmployeeClassNameIdQuery, IEnumerable<EmployeeClassNameIdDTO>>
    {
        private readonly IEmployeeClassRepository _employeeClassRepository;

        public GetAllEmployeeClassNameIdQueryHandler(IEmployeeClassRepository employeeClassRepository)
        {
            _employeeClassRepository = employeeClassRepository;
        }

        public async Task<IEnumerable<EmployeeClassNameIdDTO>> Handle(GetAllEmployeeClassNameIdQuery request, CancellationToken cancellationToken)
        {
            var employeeClasses = await _employeeClassRepository.GetAllEmployeeClassNameIdAsync();

            return employeeClasses.Select(ec => new EmployeeClassNameIdDTO(ec.EmployeeClassId, ec.EmployeeClassName));
        }
    }
}
