using WMS.Practice.Application.Queries.MaterialQueries.Materials;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialLotsByWarehouseIdQueryHandler : IRequestHandler<GetMaterialLotsByWarehouseIdQuery, IEnumerable<MaterialLotDTO>>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetMaterialLotsByWarehouseIdQueryHandler(IMaterialLotRepository materialLotRepository, IMapper mapper, IMediator mediator)
        {
            _materialLotRepository = materialLotRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<MaterialLotDTO>> Handle(GetMaterialLotsByWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            var materials = await _mediator.Send(new GetMaterialsByWarehouseIdQuery(request.WarehouseId), cancellationToken);

            var materialLots = new List<MaterialLot>();
            foreach (var material in materials)
            {
                var lots = await _materialLotRepository.GetMaterialLotsByMaterialId(material.MaterialId);
                if (lots?.Count > 0)
                    materialLots.AddRange(lots);
            }

            if (materialLots.Count == 0)
                throw new EntityNotFoundException($"Don't Have MaterialLots with WarehouseId is {request.WarehouseId}");

            return _mapper.Map<IEnumerable<MaterialLotDTO>>(materialLots);
        }
    }
}
