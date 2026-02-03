using WMS.Practice.Domain.AggregateModels.MaterialAggregate;

namespace WMS.Practice.Application.Commands.MaterialCommands.Materials
{
    public class CreateMaterialPropertyCommandHandler : IRequestHandler<CreateMaterialPropertyCommand, bool>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMaterialPropertyRepository _materialPropertyRepository;
        public CreateMaterialPropertyCommandHandler(IMaterialPropertyRepository materialPropertyRepository, IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
            _materialPropertyRepository = materialPropertyRepository;
        }

        public async Task<bool> Handle(CreateMaterialPropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _materialRepository.ExistAsync(request.MaterialId) is false)
            {
                throw new EntityNotFoundException("Material could not found", nameof(request.MaterialId));
            }

            if (await _materialPropertyRepository.ExistAsync(request.PropertyId) is true)
            {
                throw new DuplicateRecordException("Material Property is duplicated", nameof(request.PropertyId));
            }

            var newMaterialProperty = new MaterialProperty(propertyId: request.PropertyId,
                                                           propertyName: request.PropertyName,
                                                           propertyValue: request.PropertyValue,
                                                           unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                           materialId: request.MaterialId);

            _materialPropertyRepository.Create(newMaterialProperty);
            return await _materialPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
