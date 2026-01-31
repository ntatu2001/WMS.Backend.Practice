namespace WMS.Practice.Application.Commands.StorageCommands.Warehouses
{
    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, bool>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<bool> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId)
                         ?? throw new EntityNotFoundException("Warehouse could not found", nameof(request.WarehouseId));

            warehouse.UpdateWarehouse(warehouseId: request.WarehouseId,
                                      warehouseName: request.WarehouseName);

            return await _warehouseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
