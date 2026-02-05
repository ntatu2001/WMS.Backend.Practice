namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialLots
{
    public class CreateMaterialLotPropertyCommandHandler : IRequestHandler<CreateMaterialLotPropertyCommand, bool>
    {
        private readonly IMaterialLotRepository _materialLotRepository;
        private readonly IMaterialLotPropertyRepository _materialLotPropertyRepository;

        public CreateMaterialLotPropertyCommandHandler(IMaterialLotPropertyRepository materialLotPropertyRepository, IMaterialLotRepository materialLotRepository)
        {
            _materialLotPropertyRepository = materialLotPropertyRepository;
            _materialLotRepository = materialLotRepository;
        }

        public async Task<bool> Handle(CreateMaterialLotPropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _materialLotRepository.ExistAsync(request.MaterialLotId) is false)
            {
                throw new EntityNotFoundException(nameof(MaterialLotProperty), request.PropertyId);
            }

            if (await _materialLotPropertyRepository.ExistAsync(request.PropertyId) is true)
            {
                throw new DuplicateRecordException(nameof(MaterialLotProperty), request.PropertyId);
            }

            var newMaterialLotProperty = new MaterialLotProperty(propertyId: request.PropertyId,
                                                                 propertyName: request.PropertyName,
                                                                 propertyValue: request.PropertyValue,
                                                                 materialLotId: request.MaterialLotId,
                                                                 unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>());

            _materialLotPropertyRepository.Create(newMaterialLotProperty);
            return await _materialLotPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

    }
}
