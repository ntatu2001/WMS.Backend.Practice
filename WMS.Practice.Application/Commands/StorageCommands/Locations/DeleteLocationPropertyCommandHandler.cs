namespace WMS.Practice.Application.Commands.StorageCommands.Locations
{
    public class DeleteLocationPropertyCommandHandler : IRequestHandler<DeleteLocationPropertyCommand, bool>
    {
        private readonly ILocationPropertyRepository _locationPropertyRepository;

        public DeleteLocationPropertyCommandHandler(ILocationPropertyRepository locationPropertyRepository)
        {
            _locationPropertyRepository = locationPropertyRepository;
        }

        public async Task<bool> Handle(DeleteLocationPropertyCommand request, CancellationToken cancellationToken)
        {
            var locationProperty = await _locationPropertyRepository.GetLocationPropertyByIdAsync(request.LocationPropertyId)
                                ?? throw new EntityNotFoundException("Location Property not found", nameof(request.LocationPropertyId));

            _locationPropertyRepository.Remove(locationProperty);
            return await _locationPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
