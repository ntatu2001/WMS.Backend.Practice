namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetAllEmployeePropertiesQueryHandler : IRequestHandler<GetAllEmployeePropertiesQuery, IEnumerable<EmployeePropertyDTO>>
    {
        private readonly IEmployeePropertyRepository _employeePropertyRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeePropertiesQueryHandler(IEmployeePropertyRepository employeePropertyRepository, IMapper mapper)
        {
            _employeePropertyRepository = employeePropertyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeePropertyDTO>> Handle(GetAllEmployeePropertiesQuery request, CancellationToken cancellationToken)
        {
            var employeeProperties = await _employeePropertyRepository.GetAllEmployeeProperties()
                                  ?? throw new EntityNotFoundException("Employee Properties could not found");

            var employeePropertyDTOs = _mapper.Map<IEnumerable<EmployeePropertyDTO>>(employeeProperties);
            return employeePropertyDTOs;
        }
    }
}
