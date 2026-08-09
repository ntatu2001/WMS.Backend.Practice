using WMS.Practice.Application.DTOs.PersonDTOs.EmployeeClasses;

namespace WMS.Practice.Application.Queries.PersonQueries.EmployeeClasses
{
    public class GetEmployeeClassByIdQueryHandler : IRequestHandler<GetEmployeeClassByIdQuery, EmployeeClassDTO>
    {
        private readonly IEmployeeClassRepository _employeeClassRepository;
        private readonly IMapper _mapper;

        public GetEmployeeClassByIdQueryHandler(IEmployeeClassRepository employeeClassRepository, IMapper mapper)
        {
            _employeeClassRepository = employeeClassRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeClassDTO> Handle(GetEmployeeClassByIdQuery request, CancellationToken cancellationToken)
        {
            var existingEmployeeClass = await _employeeClassRepository.GetEmployeeClassByIdAsync(request.EmployeeClassId)
                                     ?? throw new EntityNotFoundException($"Employee Class with Class Id {request.EmployeeClassId} could not found", nameof(request.EmployeeClassId));

            return _mapper.Map<EmployeeClassDTO>(existingEmployeeClass);
        }
    }
}
