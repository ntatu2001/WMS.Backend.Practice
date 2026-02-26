namespace WMS.Practice.Application.Commands.MaterialCommands.Materials
{
    public class UpdateMaterialPropertyCommandHandler : IRequestHandler<UpdateMaterialPropertyCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMaterialPropertyRepository _materialPropertyRepository;
        public UpdateMaterialPropertyCommandHandler(IMaterialPropertyRepository materialPropertyRepository, IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
            _materialPropertyRepository = materialPropertyRepository;
        }

        public async Task<bool> Handle(UpdateMaterialPropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _materialRepository.ExistAsync(request.MaterialId) is false)
            {
                throw new EntityNotFoundException("Material could not found", nameof(request.MaterialId));
            }

            var existingProperty = await _materialPropertyRepository.GetMaterialPropertyByPropertyIdAsync(request.PropertyId)
                                ?? throw new EntityNotFoundException("Material Property could not found", nameof(request.PropertyId));

            existingProperty.Update(propertyName: request.PropertyName,
                                    propertyValue: request.PropertyValue,
                                    unitOfMeasure: request.UnitOfMeasure?.ParseEnum<UnitOfMeasure>());

            _materialPropertyRepository.Update(existingProperty);
            return await _materialPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
