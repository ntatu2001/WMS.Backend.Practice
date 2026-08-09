using WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses;

namespace WMS.Practice.Application.Queries.PersonQueries.EmployeeClasses
{
    public class GetAllEmployeeClassesQueryHandler : IRequestHandler<GetAllEmployeeClassesQuery, IEnumerable<EmployeeClassDTO>>
    {
        private readonly IEmployeeClassRepository _employeeClassRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeeClassesQueryHandler(IEmployeeClassRepository employeeClassRepository, IMapper mapper)
        {
            _employeeClassRepository = employeeClassRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeClassDTO>> Handle(GetAllEmployeeClassesQuery request, CancellationToken cancellationToken)
        {
            var employeeClasses = await _employeeClassRepository.GetAllAsync()
                               ?? throw new EntityNotFoundException("Employee Classes could not found");

            return _mapper.Map<IEnumerable<EmployeeClassDTO>>(employeeClasses);
        }
    }
}
