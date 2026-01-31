namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class UpdateLocationPropertyCommandHandler : IRequestHandler<UpdateLocationPropertyCommand, bool>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILocationPropertyRepository _locationPropertyRepository;

        public UpdateLocationPropertyCommandHandler(ILocationRepository locationRepository, ILocationPropertyRepository locationPropertyRepository)
        {
            _locationRepository = locationRepository;
            _locationPropertyRepository = locationPropertyRepository;
        }

        public async Task<bool> Handle(UpdateLocationPropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _locationRepository.ExistsAsync(request.LocationId) is false)
            {
                throw new EntityNotFoundException("Location not found", nameof(request.LocationId));
            }

            var locationProperty = await _locationPropertyRepository.GetLocationPropertyByIdAsync(request.PropertyId)
                                ?? throw new EntityNotFoundException("Location Property not found", nameof(request.PropertyId));

            locationProperty.UpdateLocationProperty(propertyName: request.PropertyName,
                                                    propertyValue: request.PropertyValue,
                                                    unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>());

            return await _locationPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
