namespace WMS.Practice.Application.Queries.PersonQueries.Employees
{
    public class GetEmployeePropertyByIdQueryHandler : IRequestHandler<GetEmployeePropertyByIdQuery, EmployeePropertyDTO>
    {
        private readonly IEmployeePropertyRepository _employeePropertyRepository;
        private readonly IMapper _mapper;
        public GetEmployeePropertyByIdQueryHandler(IEmployeePropertyRepository employeePropertyRepository, IMapper mapper)
        {
            _employeePropertyRepository = employeePropertyRepository;
            _mapper = mapper;
        }

        public async Task<EmployeePropertyDTO> Handle(GetEmployeePropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var existingEmployeeProperty = await _employeePropertyRepository.GetByIdAsync(request.EmployeePropertyId)
                                        ?? throw new EntityNotFoundException($"Employee Property {request.EmployeePropertyId}", nameof(request.EmployeePropertyId));

            var employeePropertyDTO = _mapper.Map<EmployeePropertyDTO>(existingEmployeeProperty);
            return employeePropertyDTO;
        }
    }
}
