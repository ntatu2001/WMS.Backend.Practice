namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialSubLots
{
    public class MoveMaterialSubLotCommandHandler : IRequestHandler<MoveMaterialSubLotCommand, bool>
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IStockTakeRepository _stockTakeRepository;
        private readonly IStockLocationHistoryRepository _stockLocationHistoryRepository;
        private readonly ILocationCapacityService _locationCapacityService;

        public MoveMaterialSubLotCommandHandler(IMaterialSubLotRepository materialSubLotRepository, ILocationRepository locationRepository,
                                                 IStockTakeRepository stockTakeRepository, IStockLocationHistoryRepository stockLocationHistoryRepository,
                                                 ILocationCapacityService locationCapacityService)
        {
            _materialSubLotRepository = materialSubLotRepository;
            _locationRepository = locationRepository;
            _stockTakeRepository = stockTakeRepository;
            _stockLocationHistoryRepository = stockLocationHistoryRepository;
            _locationCapacityService = locationCapacityService;
        }

        public async Task<bool> Handle(MoveMaterialSubLotCommand request, CancellationToken cancellationToken)
        {
            var materialSubLot = await _materialSubLotRepository.GetMaterialSubLotByIdAsync(request.MaterialSubLotId)
                              ?? throw new EntityNotFoundException(nameof(MaterialSubLot), request.MaterialSubLotId);

            if (materialSubLot.LocationId == request.ToLocationId)
                return true;

            var previousLocationId = materialSubLot.LocationId;

            var destinationLocation = await _locationRepository.GetLocationByIdAsync(request.ToLocationId)
                                   ?? throw new EntityNotFoundException(nameof(Location), request.ToLocationId);

            if (materialSubLot.Location.WarehouseId != destinationLocation.WarehouseId)
                throw new Exception($"Cannot move MaterialSubLot {request.MaterialSubLotId} to Location {request.ToLocationId}: " +
                    $"source and destination are in different warehouses");

            if (await _stockTakeRepository.ExistsPendingStockTakeByLotNumberAsync(materialSubLot.LotNumber))
                throw new Exception($"Cannot move MaterialSubLot {request.MaterialSubLotId}: LotNumber {materialSubLot.LotNumber} " +
                    $"has a pending StockTake. Please complete or cancel it before moving this sublot.");

            var (destinationUsedVolume, destinationMaxVolume) = await _locationCapacityService.CalculateLocationVolumeAsync(destinationLocation, excludeSubLotId: materialSubLot.MaterialSubLotId);

            var material = await _materialSubLotRepository.GetMaterialBySubLotIdAsync(materialSubLot.MaterialSubLotId)
                        ?? throw new EntityNotFoundException(nameof(Material), materialSubLot.MaterialSubLotId);

            if (material.TryCalculateUsedVolume(materialSubLot.ExistingQuantity, out double incomingVolume) is false)
                throw new Exception($"Cannot calculate used volume for MaterialSubLot {request.MaterialSubLotId}: " +
                    $"Material is missing PacketSize/VolumePacket properties");

            var resultingUsedVolume = destinationUsedVolume + incomingVolume;
            var resultingRate = destinationMaxVolume > 0 ? (resultingUsedVolume / destinationMaxVolume) * 100 : 100.0;

            if (resultingRate >= 100.0)
                throw new LocationCapacityExceededException(locationId: request.ToLocationId,
                                                             maxVolume: destinationMaxVolume,
                                                             currentUsedVolume: destinationUsedVolume,
                                                             incomingVolume: incomingVolume,
                                                             resultingRate: resultingRate);

            materialSubLot.Update(subLotStatus: null, existingQuality: null, unitOfMeasure: null, locationId: request.ToLocationId);

            var eventDate = DateTime.UtcNow;

            _stockLocationHistoryRepository.Create(new StockLocationHistory(stockLocationHistoryId: Guid.NewGuid().ToString(),
                                                                            materialSubLotId: materialSubLot.MaterialSubLotId,
                                                                            lotNumber: materialSubLot.LotNumber,
                                                                            locationId: previousLocationId,
                                                                            quantity: materialSubLot.ExistingQuantity,
                                                                            movementType: StockMovementType.Outbound,
                                                                            eventDate: eventDate));

            _stockLocationHistoryRepository.Create(new StockLocationHistory(stockLocationHistoryId: Guid.NewGuid().ToString(),
                                                                            materialSubLotId: materialSubLot.MaterialSubLotId,
                                                                            lotNumber: materialSubLot.LotNumber,
                                                                            locationId: request.ToLocationId,
                                                                            quantity: materialSubLot.ExistingQuantity,
                                                                            movementType: StockMovementType.Inbound,
                                                                            eventDate: eventDate));

            _materialSubLotRepository.Update(materialSubLot);

            return await _materialSubLotRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
