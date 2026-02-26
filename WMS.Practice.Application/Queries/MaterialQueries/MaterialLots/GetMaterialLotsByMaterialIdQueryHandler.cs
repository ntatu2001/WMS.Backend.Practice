namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotsByMaterialIdQueryHandler : IRequestHandler<GetMaterialLotsByMaterialIdQuery, IEnumerable<MaterialLotDTO>>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMaterialLotsByMaterialIdQueryHandler(IMaterialLotRepository materialLotRepository, IMapper mapper, IMediator mediator)
        {
            _materialLotRepository = materialLotRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<MaterialLotDTO>> Handle(GetMaterialLotsByMaterialIdQuery request, CancellationToken cancellationToken)
        {
            var materialLots = await _materialLotRepository.GetMaterialLotsByMaterialId(request.MaterialId)
                            ?? throw new EntityNotFoundException($"Material Lots could not found with Material Id {request.MaterialId}");

            var availbleMaterialLots = new List<MaterialLot>();
            foreach (var materialLot in materialLots)
            {
                var quantityDTO = await _mediator.Send(new GetQuantityByMaterialLotIdQuery(materialLot.LotNumber), cancellationToken);
                if (quantityDTO.AvailableQuantity > 0)
                {
                    availbleMaterialLots.Add(materialLot);
                }
            }

            return _mapper.Map<IEnumerable<MaterialLotDTO>>(availbleMaterialLots);
        }
    }
}
