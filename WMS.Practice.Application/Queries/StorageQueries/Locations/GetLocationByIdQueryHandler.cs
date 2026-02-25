namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationDTO>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetLocationByIdQueryHandler(ILocationRepository locationRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<LocationDTO> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationByIdAsync(request.LocationId)
                        ?? throw new EntityNotFoundException(nameof(Location), request.LocationId);

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(location.WarehouseId)
                         ?? throw new EntityNotFoundException(nameof(Warehouse), location.WarehouseId);

            var locationDTO = _mapper.Map<LocationDTO>(location);
            locationDTO.UpdateWarehouseName(warehouse.WarehouseName);

            return locationDTO;
        }
    }
}
