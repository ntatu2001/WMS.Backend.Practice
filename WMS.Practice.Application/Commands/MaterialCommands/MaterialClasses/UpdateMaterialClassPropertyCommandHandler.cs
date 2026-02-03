namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class UpdateMaterialClassPropertyCommandHandler : IRequestHandler<UpdateMaterialClassPropertyCommand, bool>
    {
        private readonly IMaterialClassRepository _materialClassRepository;
        private readonly IMaterialClassPropertyRepository _propertyRepository;
        public UpdateMaterialClassPropertyCommandHandler(IMaterialClassPropertyRepository propertyRepository, IMaterialClassRepository materialClassRepository)
        {
            _propertyRepository = propertyRepository;
            _materialClassRepository = materialClassRepository;
        }

        public async Task<bool> Handle(UpdateMaterialClassPropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _materialClassRepository.ExistsAsync(request.MaterialClassId) is false)
            {
                throw new EntityNotFoundException("Material Class could not found", nameof(request.MaterialClassId));
            }

            var existingProperty = await _propertyRepository.GetByIdAsync(request.PropertyId)
                                ?? throw new EntityNotFoundException("Material Class Property could not found", nameof(request.PropertyId));

            existingProperty.Update(propertyName: request.PropertyName,
                                    propertyValue: request.PropertyValue,
                                    unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>());

            _propertyRepository.Update(existingProperty);
            return await _propertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
