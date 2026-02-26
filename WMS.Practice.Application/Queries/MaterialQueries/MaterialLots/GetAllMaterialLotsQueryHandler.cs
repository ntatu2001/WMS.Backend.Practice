namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetAllMaterialLotsQueryHandler : IRequestHandler<GetAllMaterialLotsQuery, IEnumerable<MaterialLotDTO>>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMapper _mapper;
        public GetAllMaterialLotsQueryHandler(IMaterialLotRepository materialLotRepository, IMapper mapper)
        {
            _materialLotRepository = materialLotRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MaterialLotDTO>> Handle(GetAllMaterialLotsQuery request, CancellationToken cancellationToken)
        {
            var materialLots = await _materialLotRepository.GetAllMaterialLotsAsync()
                            ?? throw new EntityNotFoundException("Material lots could not found");

            return _mapper.Map<List<MaterialLotDTO>>(materialLots);
        }
    }
}
