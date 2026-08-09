namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class CreateMaterialSubLotCommandHandler : IRequestHandler<CreateMaterialSubLotCommand, bool>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly IStockLocationHistoryRepository _stockLocationHistoryRepository;

        public CreateMaterialSubLotCommandHandler(IMaterialSubLotRepository materialSubLotRepository, IStockLocationHistoryRepository stockLocationHistoryRepository)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _stockLocationHistoryRepository = stockLocationHistoryRepository;
        }

        public async Task<bool> Handle(CreateMaterialSubLotCommand request, CancellationToken cancellationToken)
        {
            if (await _materialSubLotRepository.ExistsAsync(request.SubLotId))
            {
                throw new DuplicateRecordException(nameof(MaterialSubLot), request.SubLotId);
            }

            var newMaterialSubLot = new MaterialSubLot(materialSubLotId: request.SubLotId,
                                                       subLotStatus: request.SubLotStatus.ParseEnum<LotStatus>(),
                                                       existingQuantity: request.ExistingQuantity,
                                                       unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                       locationId: request.LocationId,
                                                       lotNumber: request.LotNumber);

            _materialSubLotRepository.Create(newMaterialSubLot);

            _stockLocationHistoryRepository.Create(new StockLocationHistory(stockLocationHistoryId: Guid.NewGuid().ToString(),
                                                                            materialSubLotId: newMaterialSubLot.MaterialSubLotId,
                                                                            lotNumber: newMaterialSubLot.LotNumber,
                                                                            locationId: newMaterialSubLot.LocationId,
                                                                            quantity: newMaterialSubLot.ExistingQuantity,
                                                                            movementType: StockMovementType.Inbound,
                                                                            eventDate: DateTime.UtcNow));

            return await _materialSubLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
