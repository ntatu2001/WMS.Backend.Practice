namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotsByStatusQueryHandler : IRequestHandler<GetMaterialSubLotsByStatusQuery, IEnumerable<MaterialSubLotDTO>>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMapper _mapper;
        public GetMaterialSubLotsByStatusQueryHandler(IMaterialSubLotRepository materialSubLotRepository, IMapper mapper)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MaterialSubLotDTO>> Handle(GetMaterialSubLotsByStatusQuery request, CancellationToken cancellationToken)
        {
            var lotStatus = request.Status.ParseEnum<LotStatus>();
            var materialSubLots = await _materialSubLotRepository.GetMaterialSubLotsByStatusAsync(lotStatus)
                               ?? throw new EntityNotFoundException($"Material sublots with status {request.Status} not found");

            return _mapper.Map<IEnumerable<MaterialSubLotDTO>>(materialSubLots);
        }
    }
}
