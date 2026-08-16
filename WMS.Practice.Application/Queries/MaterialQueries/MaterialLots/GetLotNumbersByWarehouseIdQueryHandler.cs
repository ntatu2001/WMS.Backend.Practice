using WMS.Practice.Application.Queries.MaterialQueries.Materials;

namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetLotNumbersByWarehouseIdQueryHandler : IRequestHandler<GetLotNumbersByWarehouseIdQuery, IEnumerable<string>>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMediator _mediator;

        public GetLotNumbersByWarehouseIdQueryHandler(IMaterialLotRepository materialLotRepository, IMediator mediator)
        {
            _materialLotRepository = materialLotRepository;
            _mediator = mediator;
        }

        public async Task<IEnumerable<string>> Handle(GetLotNumbersByWarehouseIdQuery request, CancellationToken cancellationToken)
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

            return materialLots.Where(lot => lot.ExistingQuantity > 0)
                                .Select(lot => lot.LotNumber);
        }
    }
}
