namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialLots
{
    public class UpdateMaterialLotCommandHandler : IRequestHandler<UpdateMaterialLotCommand, bool>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IStockLocationHistoryRepository _stockLocationHistoryRepository;
        public UpdateMaterialLotCommandHandler(IMaterialLotRepository materialLotRepository, IStockLocationHistoryRepository stockLocationHistoryRepository)
        {
            _materialLotRepository = materialLotRepository;
            _stockLocationHistoryRepository = stockLocationHistoryRepository;
        }

        public async Task<bool> Handle(UpdateMaterialLotCommand request, CancellationToken cancellationToken)
        {
            var existingMaterialLot = await _materialLotRepository.GetMaterialLotByIdAsync(request.LotNumber)
                                   ?? throw new EntityNotFoundException(nameof(MaterialLot), request.LotNumber);

            existingMaterialLot.Update(lotStatus: request.LotStatus.ParseEnum<LotStatus>(),
                                       existingQuantity: request.ExisitingQuantity);

            foreach (var property in request.Properties)
            {
                var unitOfMeasure = property.UnitOfMeasure?.ParseEnum<UnitOfMeasure>();
                var updatingResult = existingMaterialLot.TryUpdateProperty(propertyId: property.PropertyId, 
                                                                           propertyName: property.PropertyName, 
                                                                           propertyValue: property.PropertyValue, 
                                                                           unitOfMeasure: unitOfMeasure);
                if (updatingResult is false)
                {
                    throw new InvalidOperationException("Material Lot Property could not be updated");
                }
            }

            foreach (var subLot in request.SubLots)
            {
                var existingSubLot = existingMaterialLot.SubLots?.FirstOrDefault(x => x.MaterialSubLotId == subLot.SubLotId);
                var previousLocationId = existingSubLot?.LocationId;

                var subLotStatus = subLot.SubLotStatus?.ParseEnum<LotStatus>();
                var unitOfMeasure = subLot.UnitOfMeasure?.ParseEnum<UnitOfMeasure>();
                var updatingResult = existingMaterialLot.TryUpdateSubLot(subLotId: subLot.SubLotId,
                                                                         subLotStatus: subLotStatus,
                                                                         existingQuantity: subLot.ExistingQuantity,
                                                                         unitOfMeasure: unitOfMeasure,
                                                                         locationId: subLot.LocationId);

                if (updatingResult is false)
                {
                    throw new InvalidOperationException("Material Lot SubLot could not be updated");
                }

                if (string.IsNullOrEmpty(subLot.LocationId) is false && previousLocationId is not null && previousLocationId != subLot.LocationId)
                {
                    var movedQuantity = existingSubLot!.ExistingQuantity;
                    var eventDate = DateTime.UtcNow;

                    _stockLocationHistoryRepository.Create(new StockLocationHistory(stockLocationHistoryId: Guid.NewGuid().ToString(),
                                                                                    materialSubLotId: existingSubLot.MaterialSubLotId,
                                                                                    lotNumber: existingSubLot.LotNumber,
                                                                                    locationId: previousLocationId,
                                                                                    quantity: movedQuantity,
                                                                                    movementType: StockMovementType.Outbound,
                                                                                    eventDate: eventDate));

                    _stockLocationHistoryRepository.Create(new StockLocationHistory(stockLocationHistoryId: Guid.NewGuid().ToString(),
                                                                                    materialSubLotId: existingSubLot.MaterialSubLotId,
                                                                                    lotNumber: existingSubLot.LotNumber,
                                                                                    locationId: subLot.LocationId,
                                                                                    quantity: movedQuantity,
                                                                                    movementType: StockMovementType.Inbound,
                                                                                    eventDate: eventDate));
                }
            }

            _materialLotRepository.Update(existingMaterialLot);
            return await _materialLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
