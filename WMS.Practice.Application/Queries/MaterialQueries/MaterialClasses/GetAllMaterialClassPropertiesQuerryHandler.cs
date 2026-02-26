using WMS.Practice.Application.DTOs.MaterialDTOs.MaterialClasses;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialClasses
{
    public class GetAllMaterialClassPropertiesQueryHandler : IRequestHandler<GetAllMaterialClassPropertiesQuery, IEnumerable<MaterialClassPropertyDTO>>
    {
        private readonly IMaterialClassPropertyRepository _materialClassPropertyRepository;
        private readonly IMapper _mapper;

        public GetAllMaterialClassPropertiesQueryHandler(IMaterialClassPropertyRepository materialClassPropertyRepository, IMapper mapper)
        {
            _materialClassPropertyRepository = materialClassPropertyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialClassPropertyDTO>> Handle(GetAllMaterialClassPropertiesQuery request, CancellationToken cancellationToken)
        {
            var materialClassProperties = await _materialClassPropertyRepository.GetAllAsync()
                                       ?? throw new EntityNotFoundException("Material Class Properties could not found");

            return _mapper.Map<IEnumerable<MaterialClassPropertyDTO>>(materialClassProperties);
        }
    }
}
