namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotsByLotNumberQueryHandler : IRequestHandler<GetMaterialSubLotsByLotNumberQuery, IEnumerable<MaterialSubLotDTO>>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMapper _mapper;

        public GetMaterialSubLotsByLotNumberQueryHandler(IMaterialSubLotRepository materialSubLotRepository, IMapper mapper)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialSubLotDTO>> Handle(GetMaterialSubLotsByLotNumberQuery request, CancellationToken cancellationToken)
        {
            var materialSubLots = await _materialSubLotRepository.GetMaterialSubLotsByLotNumberAsync(request.LotNumber)
                               ?? throw new EntityNotFoundException($"Material sublots for lot number {request.LotNumber} not found");

            return _mapper.Map<IEnumerable<MaterialSubLotDTO>>(materialSubLots);
        }
    }
}
