namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialSubLots
{
    public class GetMaterialSubLotByIdQueryHandler : IRequestHandler<GetMaterialSubLotByIdQuery, MaterialSubLotDTO>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMapper _mapper;
        public GetMaterialSubLotByIdQueryHandler(IMaterialSubLotRepository materialSubLotRepository, IMapper mapper)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _mapper = mapper;
        }
        public async Task<MaterialSubLotDTO> Handle(GetMaterialSubLotByIdQuery request, CancellationToken cancellationToken)
        {
            var materialSubLot = await _materialSubLotRepository.GetMaterialSubLotByIdAsync(request.MaterialSubLotId)
                              ?? throw new EntityNotFoundException($"Material sublot with ID {request.MaterialSubLotId} not found");

            return _mapper.Map<MaterialSubLotDTO>(materialSubLot);
        }
    }
}
