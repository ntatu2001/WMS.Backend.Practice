namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialLots
{
    public class CreateMaterialLotCommandHandler : IRequestHandler<CreateMaterialLotCommand, bool>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMaterialSubLotRepository _subLotRepository;
        private readonly IStockLocationHistoryRepository _stockLocationHistoryRepository;
        public CreateMaterialLotCommandHandler(IMaterialLotRepository materialLotRepository, IMaterialSubLotRepository subLotRepository, IStockLocationHistoryRepository stockLocationHistoryRepository)
        {
            _materialLotRepository = materialLotRepository;
            _subLotRepository = subLotRepository;
            _stockLocationHistoryRepository = stockLocationHistoryRepository;
        }

        public async Task<bool> Handle(CreateMaterialLotCommand request, CancellationToken cancellationToken)
        {
            if (await _materialLotRepository.ExistAsync(request.LotNumber) is true)
            {
                throw new DuplicateRecordException("Material Lot is duplicated", nameof(request.LotNumber));
            }

            var newMaterialLot = new MaterialLot(lotNumber: request.LotNumber,
                                                 lotStatus: request.LotStatus.ParseEnum<LotStatus>(),
                                                 existingQuantity: request.ExisitingQuantity,
                                                 materialId: request.MaterialId);

            foreach (var subLot in request.SubLots)
            {
                if (await _subLotRepository.ExistsAsync(subLot.SubLotId) is true)
                {
                    throw new DuplicateRecordException("SubLot is duplicated", nameof(subLot.SubLotId));
                }

                var newSubLot = new MaterialSubLot(materialSubLotId: subLot.SubLotId,
                                                   subLotStatus: subLot.SubLotStatus.ParseEnum<LotStatus>(),
                                                   existingQuantity: subLot.ExistingQuantity,
                                                   locationId: subLot.LocationId,
                                                   lotNumber: request.LotNumber,
                                                   unitOfMeasure: subLot.UnitOfMeasure.ParseEnum<UnitOfMeasure>());
                newMaterialLot.AddSubLot(newSubLot);

                _stockLocationHistoryRepository.Create(new StockLocationHistory(stockLocationHistoryId: Guid.NewGuid().ToString(),
                                                                                materialSubLotId: newSubLot.MaterialSubLotId,
                                                                                lotNumber: newSubLot.LotNumber,
                                                                                locationId: newSubLot.LocationId,
                                                                                quantity: newSubLot.ExistingQuantity,
                                                                                movementType: StockMovementType.Inbound,
                                                                                eventDate: DateTime.UtcNow));
            }

            _materialLotRepository.Create(newMaterialLot);
            return await _materialLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
