namespace WMS.Practice.Application.Queries.MaterialQueries.Materials
{
    public class GetMaterialByIdQueryHandler : IRequestHandler<GetMaterialByIdQuery, MaterialDTO>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GetMaterialByIdQueryHandler(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<MaterialDTO> Handle(GetMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(request.MaterialId)
                        ?? throw new EntityNotFoundException("Material could not found");

            return _mapper.Map<MaterialDTO>(material);
        }
    }
}
