namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class CreateLocationPropertyCommandHandler : IRequestHandler<CreateLocationPropertyCommand, bool>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILocationPropertyRepository _locationPropertyRepository;

        public CreateLocationPropertyCommandHandler(ILocationRepository locationRepository, ILocationPropertyRepository locationPropertyRepository)
        {
            _locationRepository = locationRepository;
            _locationPropertyRepository = locationPropertyRepository;
        }

        public async Task<bool> Handle(CreateLocationPropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _locationRepository.ExistsAsync(request.LocationId) is true)
            {
                throw new DuplicateRecordException($"Location is duplicated", nameof(request.LocationId));
            }

            if (await _locationPropertyRepository.ExistsAsync(request.PropertyId) is true)
            {
                throw new DuplicateRecordException($"Location Property is duplicated", nameof(request.PropertyId));
            }

            var locationProperty = new LocationProperty(propertyId: request.PropertyId,
                                                        propertyName: request.PropertyName,
                                                        propertyValue: request.PropertyValue,
                                                        unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                        locationId: request.LocationId);

            _locationPropertyRepository.Create(locationProperty);
            return await _locationPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
