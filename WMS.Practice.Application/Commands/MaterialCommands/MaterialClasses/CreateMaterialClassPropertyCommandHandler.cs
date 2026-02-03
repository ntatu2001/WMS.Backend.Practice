namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialClasses
{
    public class CreateMaterialClassPropertyCommandHandler : IRequestHandler<CreateMaterialClassPropertyCommand, bool>
    {
        private readonly IMaterialClassRepository _classRepository;
        private readonly IMaterialClassPropertyRepository _classPropertyRepository;
        public CreateMaterialClassPropertyCommandHandler(IMaterialClassPropertyRepository classPropertyRepository, IMaterialClassRepository classRepository)
        {
            _classRepository = classRepository;
            _classPropertyRepository = classPropertyRepository;
        }

        public async Task<bool> Handle(CreateMaterialClassPropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _classRepository.ExistsAsync(request.MaterialClassId) is false)
            {
                throw new EntityNotFoundException("Material Class could not found", nameof(request.MaterialClassId));
            }

            var newClassProperty = new MaterialClassProperty(propertyId: Guid.NewGuid().ToString(),
                                                             propertyName: request.PropertyName,
                                                             propertyValue: request.PropertyValue,
                                                             unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                             materialClassId: request.MaterialClassId);

            _classPropertyRepository.Create(newClassProperty);
            return await _classPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
