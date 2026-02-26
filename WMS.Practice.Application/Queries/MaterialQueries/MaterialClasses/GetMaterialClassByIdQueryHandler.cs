using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetMaterialClassByIdQueryHandler : IRequestHandler<GetMaterialClassByIdQuery, MaterialClassDTO>
    {
        private readonly IMaterialClassRepository _materialClassRepository;
        private readonly IMapper _mapper;

        public GetMaterialClassByIdQueryHandler(IMaterialClassRepository materialClassRepository, IMapper mapper)
        {
            _materialClassRepository = materialClassRepository;
            _mapper = mapper;
        }

        public async Task<MaterialClassDTO> Handle(GetMaterialClassByIdQuery request, CancellationToken cancellationToken)
        {
            var existingMaterialClass = await _materialClassRepository.GetMaterialClassByClassIdAsync(request.MaterialClassId)
                                     ?? throw new EntityNotFoundException($"Material Class with Class Id {request.MaterialClassId} could not found", nameof(request.MaterialClassId));
            
            return _mapper.Map<MaterialClassDTO>(existingMaterialClass);
        }
    }
}
