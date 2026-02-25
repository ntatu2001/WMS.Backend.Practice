using WMS.Practice.Application.DTOs.StorageDTOs.Warehouses;

namespace WMS.Practice.Application.Queries.StorageQueries.Warehouses
{
    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, WarehouseDTO>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetWarehouseByIdQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<WarehouseDTO> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(request.WarehouseId)
                         ?? throw new EntityNotFoundException(nameof(Warehouse), request.WarehouseId);

            var warehouseDTO = _mapper.Map<WarehouseDTO>(warehouse);
            foreach (var location in warehouseDTO.Locations)
            {
                location.UpdateWarehouseName(warehouseDTO.WarehouseName ?? "None");
            }

            return warehouseDTO;
        }
    }
}
