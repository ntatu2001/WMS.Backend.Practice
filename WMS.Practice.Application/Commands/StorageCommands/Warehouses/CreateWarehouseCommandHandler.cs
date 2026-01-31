namespace WMS.Practice.Application.Commands.StorageCommands.Warehouses
{
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, bool>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<bool> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (await _warehouseRepository.ExistsAsync(request.WarehouseId) is true)
            {
                throw new DuplicateRecordException("Warehouse is duplicated", nameof(request.WarehouseId));
            }

            var warehouse = new Warehouse(warehouseId: request.WarehouseId,
                                          warehouseName: request.WarehouseName);

            _warehouseRepository.Create(warehouse);
            return await _warehouseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
