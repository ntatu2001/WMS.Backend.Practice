namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class SearchLocationsByLocationIdQueryHandler : IRequestHandler<SearchLocationsByLocationIdQuery, QueryResult<LocationDTO>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public SearchLocationsByLocationIdQueryHandler(ILocationRepository locationRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<QueryResult<LocationDTO>> Handle(SearchLocationsByLocationIdQuery request, CancellationToken cancellationToken)
        {
            var locationsQuery = _locationRepository.QueryLocations();

            if (!string.IsNullOrWhiteSpace(request.LocationId))
            {
                locationsQuery = locationsQuery.Where(l => l.LocationId.Contains(request.LocationId));
            }

            if (!string.IsNullOrWhiteSpace(request.WarehouseId))
            {
                locationsQuery = locationsQuery.Where(l => l.WarehouseId == request.WarehouseId);
            }

            locationsQuery = locationsQuery.OrderBy(l => l.LocationId);

            var totalItems = await locationsQuery.CountAsync(cancellationToken);

            var skip = (request.Page - 1) * request.ItemsPerPage;

            var pagedLocations = await locationsQuery.Skip(skip)
                                                       .Take(request.ItemsPerPage)
                                                       .ToListAsync(cancellationToken);

            var locationDTOs = _mapper.Map<List<LocationDTO>>(pagedLocations);
            await EnrichNameForLocationDTOsAsync(locationDTOs);

            return new QueryResult<LocationDTO>(results: locationDTOs,
                                                totalItems: totalItems);
        }

        private async Task EnrichNameForLocationDTOsAsync(List<LocationDTO> locationDTOs)
        {
            const string defaultWarehouseName = "None";
            foreach (var locationDTO in locationDTOs)
            {
                if (string.IsNullOrWhiteSpace(locationDTO.WarehouseId))
                    continue;

                var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(locationDTO.WarehouseId) ?? defaultWarehouseName;
                locationDTO.UpdateWarehouseName(warehouseName);
            }
        }
    }
}
