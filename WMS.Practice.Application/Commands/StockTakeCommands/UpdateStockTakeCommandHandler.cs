namespace WMS.Practice.Application.Commands.StockTakeCommands
{
    public class UpdateStockTakeCommandHandler : IRequestHandler<UpdateStockTakeCommand, bool>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IStockTakeRepository _stockRepository;

        public UpdateStockTakeCommandHandler(IMaterialSubLotRepository materialSubLotRepository, IMaterialLotRepository materialLotRepository, IStockTakeRepository stockRepository)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _materialLotRepository = materialLotRepository;
            _stockRepository = stockRepository;
        }

        public async Task<bool> Handle(UpdateStockTakeCommand request, CancellationToken cancellationToken)
        {
            if (await _materialSubLotRepository.ExistMaterialSubLotsByLotNumber(request.LotNumber) is true)
                throw new EntityNotFoundException(nameof(MaterialSubLot), request.LotNumber);

            var materialLot = await _materialLotRepository.GetMaterialLotByIdAsync(request.LotNumber)
                           ?? throw new EntityNotFoundException(nameof(MaterialLot), request.LotNumber);

            var stockTake = await _stockRepository.GetById(request.StockTakeId)
                         ?? throw new EntityNotFoundException(nameof(StockTake), request.StockTakeId);

            foreach (var subLot in request.MaterialSubLots)
            {
                var stockTakeSubLot = new StockTakeSubLot(stockTakeSubLotId: Guid.NewGuid().ToString(),
                                                          locationId: subLot.LocationId,
                                                          materialSubLotId: subLot.MaterialSubLotId,
                                                          previousQuantity: subLot.PreviousQuantity,
                                                          adjustedQuantity: subLot.RealQuantity,
                                                          quantityDifference: subLot.RealQuantity - subLot.PreviousQuantity,
                                                          stockTakeId: request.StockTakeId);
                stockTake.AddSubLot(stockTakeSubLot);
            }

            var totalAdjustedQuantity = stockTake.GetAdjustedQuantity();
            stockTake.Update(status: AdjustmentStatus.Done,
                             previousQuantity: materialLot.ExistingQuantity,
                             adjustedQuantity: totalAdjustedQuantity);

            stockTake.Confirm(stockTake);


            return await _stockRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
