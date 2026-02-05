namespace WMS.Practice.Application.Commands.MaterialCommands.MaterialLots
{
    public class UpdateMaterialLotPropertyCommandHandler : IRequestHandler<UpdateMaterialLotPropertyCommand, bool>
    {
        private readonly IMaterialLotPropertyRepository _materialLotPropertyRepository;
        public UpdateMaterialLotPropertyCommandHandler(IMaterialLotPropertyRepository materialLotPropertyRepository)
        {
            _materialLotPropertyRepository = materialLotPropertyRepository;
        }

        public async Task<bool> Handle(UpdateMaterialLotPropertyCommand request, CancellationToken cancellationToken)
        {
            var materialLotProperty = await _materialLotPropertyRepository.GetMaterialLotPropertyById(request.PropertyId)
                                   ?? throw new EntityNotFoundException(nameof(MaterialLotProperty), request.PropertyId);

            materialLotProperty.Update(propertyName: request.PropertyName,
                                       propertyValue: request.PropertyValue,
                                       unitOfMeasure: request.UnitOfMeasure?.ParseEnum<UnitOfMeasure>());

            _materialLotPropertyRepository.Update(materialLotProperty);
            return await _materialLotPropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
