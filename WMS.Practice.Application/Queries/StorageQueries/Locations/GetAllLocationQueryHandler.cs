namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, QueryResult<LocationDTO>>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetAllLocationQueryHandler(ILocationRepository locationRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }


        public async Task<QueryResult<LocationDTO>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationRepository.GetAllLocations()     
                         ?? throw new EntityNotFoundException("Locations", "No locations found");

            // Skip the locations on the previous page (Page is sent from Request)
            var skip = (request.Page - 1) * request.ItemsPerPage;

            // Find locations should be included in current page
            var pagedLocations = locations.Skip(skip)
                                          .Take(request.ItemsPerPage)
                                          .ToList();

            var locationDTOs = _mapper.Map<List<LocationDTO>>(pagedLocations);
            await EnrichNameForLocationDTOsAsync(locationDTOs);

            return new QueryResult<LocationDTO>(results: locationDTOs,
                                                totalItems: locations.Count);
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
