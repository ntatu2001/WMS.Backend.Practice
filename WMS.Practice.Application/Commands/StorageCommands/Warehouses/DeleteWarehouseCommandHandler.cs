namespace WMS.Practice.Application.Commands.StorageCommands.Warehouses
{
    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, bool>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public DeleteWarehouseCommandHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<bool> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId)
                         ?? throw new EntityNotFoundException("Warehouse not found", nameof(request.WarehouseId));

            _warehouseRepository.Delete(warehouse);
            return await _warehouseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
