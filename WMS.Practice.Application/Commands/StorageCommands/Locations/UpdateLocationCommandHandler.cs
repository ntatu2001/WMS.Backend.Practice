namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, bool>
    {
        private readonly ILocationRepository _locationRepository;

        public UpdateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<bool> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationByIdAsync(request.LocationId)
                        ?? throw new EntityNotFoundException($"Location with ID {request.LocationId} not found.", nameof(request.LocationId));

            location.UpdateWarehouse(request.WarehouseId);
            return await _locationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
