namespace WMS.Practice.Application.Queries.StorageQueries.Warehouses
{
    public class GetWarehouseIdByWarehouseNameQueryHandler : IRequestHandler<GetWarehouseIdByWarehouseNameQuery, List<string>>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public GetWarehouseIdByWarehouseNameQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<List<string>> Handle(GetWarehouseIdByWarehouseNameQuery request, CancellationToken cancellationToken)
        {
            var warehouseIds = await _warehouseRepository.GetWarehouseIdByWarehouseNameAsync(request.WarehouseName)
                            ?? throw new EntityNotFoundException(nameof(Warehouse), request.WarehouseName);

            return warehouseIds;
        }
    }
}
