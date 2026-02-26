namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetQuantityByMaterialLotIdQueryHandler : IRequestHandler<GetQuantityByMaterialLotIdQuery, MaterialLotQuantityDTO>
    {
        private readonly IMaterialLotRepository _materialLotRepository;

        public GetQuantityByMaterialLotIdQueryHandler(IMaterialLotRepository materialLotRepository)
        {
            _materialLotRepository = materialLotRepository;
        }

        public async Task<MaterialLotQuantityDTO> Handle(GetQuantityByMaterialLotIdQuery request, CancellationToken cancellationToken)
        {
            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(request.MaterialLotId)
                           ?? throw new EntityNotFoundException(nameof(MaterialLot), request.MaterialLotId);

            var totalIssueQuantity = 0.0;
            var issueLots = materialLot.IssueLots.Where(s => s.IsDone() is false);
            if (issueLots?.Count() > 0)
                totalIssueQuantity = issueLots.Sum(s => s.RequestedQuantity);

            double existingQuality = materialLot.SubLots.Sum(s => s.ExistingQuantity);
            double availableQuantity = existingQuality - totalIssueQuantity;
            if (availableQuantity < 0)
                throw new Exception($"Material lot {materialLot.LotNumber} has no available quantity.");

            return new MaterialLotQuantityDTO(lotNumber: materialLot.LotNumber,
                                              availableQuantity: availableQuantity);
        }
    }
}
