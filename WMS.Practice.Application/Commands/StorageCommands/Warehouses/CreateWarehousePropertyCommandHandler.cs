namespace WMS.Practice.Application.Commands.StorageCommands.Warehouses
{
    public class CreateWarehousePropertyCommandHandler : IRequestHandler<CreateWarehousePropertyCommand, bool>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehousePropertyRepository _warehousePropertyRepository;
        public CreateWarehousePropertyCommandHandler(IWarehouseRepository warehouseRepository, IWarehousePropertyRepository warehousePropertyRepository)
        {
            _warehouseRepository = warehouseRepository;
            _warehousePropertyRepository = warehousePropertyRepository;
        }

        public async Task<bool> Handle(CreateWarehousePropertyCommand request, CancellationToken cancellationToken)
        {
            if (await _warehouseRepository.ExistsAsync(request.WarehouseId) is false)
            {
                throw new EntityNotFoundException("Warehouse not found", nameof(request.WarehouseId));
            }

            if (await _warehousePropertyRepository.ExistsAsync(request.PropertyId) is true)
            {
                throw new DuplicateRecordException("Warehouse Propery is duplicated", nameof(request.PropertyId));
            }

            var warehouseProperty = new WarehouseProperty(propertyId: request.PropertyId,
                                                          propertyName: request.PropertyName,
                                                          propertyValue: request.PropertyValue,
                                                          unitOfMeasure: request.UnitOfMeasure.ParseEnum<UnitOfMeasure>(),
                                                          warehouseId: request.WarehouseId);

            _warehousePropertyRepository.Create(warehouseProperty);
            return await _warehousePropertyRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
