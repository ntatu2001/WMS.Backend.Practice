namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class UpdateMaterialSubLotsCommandHandler : IRequestHandler<UpdateMaterialSubLotsCommand, bool>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IStockTakeRepository _stockRepository;

        public UpdateMaterialSubLotsCommandHandler(IMaterialSubLotRepository materialSubLotRepository, IMaterialLotRepository materialLotRepository, IStockTakeRepository stockRepository)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _materialLotRepository = materialLotRepository;
            _stockRepository = stockRepository;
        }

        public async Task<bool> Handle(UpdateMaterialSubLotsCommand request, CancellationToken cancellationToken)
        {
            var materialSubLots = await _materialSubLotRepository.GetMaterialSubLotsByLotNumberAsync(request.LotNumber)
                               ?? throw new EntityNotFoundException(nameof(MaterialSubLot), request.LotNumber);

            var totalQuantity = 0.0;
            foreach (var subLot in request.MaterialSubLots)
            {
                var materialSubLot = await _materialSubLotRepository.GetMaterialSubLotByLotNumberAndLocationId(request.LotNumber, subLot.LocationId)
                                  ?? throw new EntityNotFoundException(nameof(MaterialSubLot), nameof(request.LotNumber));

                materialSubLot.UpdateExistingQuantity(subLot.RealQuantity);
                totalQuantity += materialSubLot.ExistingQuantity;
            }

            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(request.LotNumber)
                           ?? throw new EntityNotFoundException(nameof(MaterialLot), request.LotNumber);

            materialLot.Update(existingQuantity: totalQuantity);

            var stockTake = await _stockRepository.GetById(request.MaterialLotAdjustmentId)
                         ?? throw new EntityNotFoundException(nameof(StockTake), request.MaterialLotAdjustmentId);

            stockTake.Status = AdjustmentStatus.Done;
            stockTake.AdjustedQuantity = totalQuantity;

            return await _materialLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
