namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetAllMaterialSubLotsQueryHandler : IRequestHandler<GetAllMaterialSubLotsQuery, IEnumerable<MaterialSubLotDTO>>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMapper _mapper;

        public GetAllMaterialSubLotsQueryHandler(IMaterialSubLotRepository materialSubLotRepository, IMapper mapper)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialSubLotDTO>> Handle(GetAllMaterialSubLotsQuery request, CancellationToken cancellation)
        {
            var materialSubLots = await _materialSubLotRepository.GetAllMaterialSubLotsAsync()
                               ?? throw new EntityNotFoundException("Material sublots not found");

            return _mapper.Map<IEnumerable<MaterialSubLotDTO>>(materialSubLots);
        }
    }
}
