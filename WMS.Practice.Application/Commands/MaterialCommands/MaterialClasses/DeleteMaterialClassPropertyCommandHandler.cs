namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class DeleteMaterialClassPropertyCommandHandler : IRequestHandler<DeleteMaterialClassPropertyCommand, bool>
    {
        private readonly IMaterialClassPropertyRepository _propertyRepository;
        public DeleteMaterialClassPropertyCommandHandler(IMaterialClassPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<bool> Handle(DeleteMaterialClassPropertyCommand request, CancellationToken cancellationToken)
        {
            var existingProperty = await _propertyRepository.GetByIdAsync(request.MaterialClassPropertyId)
                                ?? throw new EntityNotFoundException("Material Class Entity could not found", nameof(request.MaterialClassPropertyId));

            _propertyRepository.Delete(existingProperty);
            return await _propertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
