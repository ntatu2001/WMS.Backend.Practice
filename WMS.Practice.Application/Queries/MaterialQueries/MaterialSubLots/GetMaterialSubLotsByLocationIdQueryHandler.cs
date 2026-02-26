namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotsByLocationIdQueryHandler : IRequestHandler<GetMaterialSubLotsByLocationIdQuery, IEnumerable<MaterialSubLotDTO>>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMapper _mapper;

        public GetMaterialSubLotsByLocationIdQueryHandler(IMaterialSubLotRepository materialSubLotRepository, IMapper mapper)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialSubLotDTO>> Handle(GetMaterialSubLotsByLocationIdQuery request, CancellationToken cancellationToken)
        {
            var materialSubLots = await _materialSubLotRepository.GetMaterialSubLotsByLocationIdAsync(request.LocationId)
                               ?? throw new EntityNotFoundException($"Material sublots for location ID {request.LocationId} not found");

            return _mapper.Map<IEnumerable<MaterialSubLotDTO>>(materialSubLots);
        }
    }
}
