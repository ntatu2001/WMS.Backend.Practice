namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialPropertyByIdQueryHandler : IRequestHandler<GetMaterialPropertyByIdQuery, MaterialPropertyDTO>
    {
        private readonly IMaterialPropertyRepository _materialPropertyRepository;
        private readonly IMapper _mapper;

        public GetMaterialPropertyByIdQueryHandler(IMaterialPropertyRepository materialPropertyRepository, IMapper mapper)
        {
            _materialPropertyRepository = materialPropertyRepository;
            _mapper = mapper;
        }

        public async Task<MaterialPropertyDTO> Handle(GetMaterialPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var property = await _materialPropertyRepository.GetMaterialPropertyByPropertyIdAsync(request.MaterialPropertyId)
                        ?? throw new EntityNotFoundException("Material Property could not found");

            return _mapper.Map<MaterialPropertyDTO>(property);
        }
    }
}
