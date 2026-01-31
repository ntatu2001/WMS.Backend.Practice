namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, bool>
    {
        private readonly ILocationRepository _locationRepository;

        public CreateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<bool> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            if (await _locationRepository.ExistsAsync(request.LocationId))
            {
                throw new DuplicateRecordException("Location", request.LocationId);
            }

            var properties = request.Properties.Select(x => CreateLocationProperty(x, request.LocationId))
                                               .ToList();

            var newLocation = new Location(locationId: request.LocationId,
                                           warehouseId: request.WarehouseId,
                                           properties: properties);

            _locationRepository.Create(newLocation);
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private static LocationProperty CreateLocationProperty(LocationPropertyCommand property, 
                                                               string locationId) => new(propertyId: Guid.NewGuid().ToString(),
                                                                                         propertyName: property.PropertyName,
                                                                                         propertyValue: property.PropertyValue,
                                                                                         unitOfMeasure: property.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                                                         locationId: locationId);
    }
}
