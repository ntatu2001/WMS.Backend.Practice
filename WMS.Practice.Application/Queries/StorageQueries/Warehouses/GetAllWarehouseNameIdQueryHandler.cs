using WMS.Practice.Application.DTOs.StorageDTOs.Warehouses;

namespace WMS.Practice.Application.Queries.StorageQueries.Warehouses
{
    public class GetAllWarehouseNameIdQueryHandler : IRequestHandler<GetAllWarehouseNameIdQuery, IEnumerable<WarehouseNameIdDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public GetAllWarehouseNameIdQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<WarehouseNameIdDTO>> Handle(GetAllWarehouseNameIdQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseRepository.GetAllWarehouseNameIdAsync();

            return warehouses.Select(w => new WarehouseNameIdDTO(w.WarehouseId, w.WarehouseName));
        }
    }
}
