using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetMaterialClassPropertyByIdQueryHandler : IRequestHandler<GetMaterialClassPropertyByIdQuery, MaterialClassPropertyDTO>
    {
        private readonly IMaterialClassPropertyRepository _materialClassPropertyRepository;
        private readonly IMapper _mapper;

        public GetMaterialClassPropertyByIdQueryHandler(IMaterialClassPropertyRepository materialClassPropertyRepository, IMapper mapper)
        {
            _materialClassPropertyRepository = materialClassPropertyRepository;
            _mapper = mapper;
        }

        public async Task<MaterialClassPropertyDTO> Handle(GetMaterialClassPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var existingMaterialClassProperty = await _materialClassPropertyRepository.GetMaterialClassPropertyByPropertyIdAsync(request.MaterialClassPropertyId)
                                             ?? throw new EntityNotFoundException($"Material Class Property with Id {request.MaterialClassPropertyId} could not found", nameof(request.MaterialClassPropertyId));
        
            return _mapper.Map<MaterialClassPropertyDTO>(existingMaterialClassProperty);
        }
    }
}
