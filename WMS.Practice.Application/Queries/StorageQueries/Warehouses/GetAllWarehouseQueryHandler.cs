using WMS.Practice.Application.DTOs.StorageDTOs.Warehouses;

namespace WMS.Practice.Application.Queries.StorageQueries.Warehouses
{
    public class GetAllWarehouseQueryHandler : IRequestHandler<GetAllWarehouseQuery, IEnumerable<WarehouseDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetAllWarehouseQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WarehouseDTO>> Handle(GetAllWarehouseQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseRepository.GetAllWarehouses()
                          ?? throw new EntityNotFoundException("Warehouses", "No warehouses found");

            var warehouseDTOs = _mapper.Map<IEnumerable<WarehouseDTO>>(warehouses);
            EnrichWarehouseNameForLocationDTOs(warehouseDTOs);

            return warehouseDTOs;
        }

        private void EnrichWarehouseNameForLocationDTOs(IEnumerable<WarehouseDTO> warehouseDTOs)
        {
            foreach (var warehouseDTO in warehouseDTOs)
            {
                if (warehouseDTO.Locations != null)
                {
                    foreach (var locationDTO in warehouseDTO.Locations)
                    {
                        locationDTO.WarehouseName = warehouseDTO.WarehouseName;
                    }
                }
            }
        }
    }
}
