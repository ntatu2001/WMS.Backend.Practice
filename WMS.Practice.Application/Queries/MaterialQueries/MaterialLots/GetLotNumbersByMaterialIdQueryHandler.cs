namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetLotNumbersByMaterialIdQueryHandler : IRequestHandler<GetLotNumbersByMaterialIdQuery, IEnumerable<string>>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMediator _mediator;

        public GetLotNumbersByMaterialIdQueryHandler(IMaterialLotRepository materialLotRepository, IMediator mediator)
        {
            _materialLotRepository = materialLotRepository;
            _mediator = mediator;
        }

        public async Task<IEnumerable<string>> Handle(GetLotNumbersByMaterialIdQuery request, CancellationToken cancellationToken)
        {
            var materialLots = await _materialLotRepository.GetMaterialLotsByMaterialId(request.MaterialId)
                            ?? throw new EntityNotFoundException($"Material Lots could not found with Material Id {request.MaterialId}");

            var lotNumbers = new List<string>();
            foreach (var materialLot in materialLots)
            {
                var quantityDTO = await _mediator.Send(new GetQuantityByMaterialLotIdQuery(materialLot.LotNumber), cancellationToken);
                if (quantityDTO.AvailableQuantity > 0)
                {
                    lotNumbers.Add(materialLot.LotNumber);
                }
            }

            return lotNumbers;
        }
    }
}
