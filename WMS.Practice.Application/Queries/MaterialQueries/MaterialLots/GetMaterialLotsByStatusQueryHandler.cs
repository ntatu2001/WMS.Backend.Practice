namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotsByStatusQueryHandler : IRequestHandler<GetMaterialLotsByStatusQuery, IEnumerable<MaterialLotDTO>>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMapper _mapper;
        public GetMaterialLotsByStatusQueryHandler(IMaterialLotRepository materialLotRepository, IMapper mapper)
        {
            _materialLotRepository = materialLotRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MaterialLotDTO>> Handle(GetMaterialLotsByStatusQuery request, CancellationToken cancellationToken)
        {
            var lotStatus = request.Status.ParseEnum<LotStatus>();
            var materialLots = await _materialLotRepository.GetMaterialLotsByStatus(lotStatus)
                              ?? throw new EntityNotFoundException($"Material lots could not found for the given status {request.Status}");

            return _mapper.Map<IEnumerable<MaterialLotDTO>>(materialLots);
        }
    }
}
