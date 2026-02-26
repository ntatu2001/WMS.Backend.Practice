namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetAllMaterialPropertiesQueryHandler : IRequestHandler<GetAllMaterialPropertiesQuery, IEnumerable<MaterialPropertyDTO>>
    {
        private readonly IMaterialPropertyRepository _materialPropertyRepository;
        private readonly IMapper _mapper;
        public GetAllMaterialPropertiesQueryHandler(IMaterialPropertyRepository materialPropertyRepository, IMapper mapper)
        {
            _materialPropertyRepository = materialPropertyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MaterialPropertyDTO>> Handle(GetAllMaterialPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _materialPropertyRepository.GetAllMaterialPropertiesAsync()
                          ?? throw new EntityNotFoundException("Material Properties could not found");

            return _mapper.Map<IEnumerable<MaterialPropertyDTO>>(properties);
        }
    }
}
