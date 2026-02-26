namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotByIdQueryHandler : IRequestHandler<GetMaterialLotByIdQuery, MaterialLotDTO>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMapper _mapper;

        public GetMaterialLotByIdQueryHandler(IMaterialLotRepository materialLotRepository, IMapper mapper)
        {
            _materialLotRepository = materialLotRepository;
            _mapper = mapper;
        }

        public async Task<MaterialLotDTO> Handle(GetMaterialLotByIdQuery request, CancellationToken cancellationToken)
        {
            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(request.MaterialLotId)
                           ?? throw new EntityNotFoundException($"Material Lot with Id {request.MaterialLotId} could not found");

            return _mapper.Map<MaterialLotDTO>(materialLot);
        }
    }
}
