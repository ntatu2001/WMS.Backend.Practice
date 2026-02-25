namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetLocationsByWarehouseIdQueryHandler : IRequestHandler<GetLocationsByWarehouseIdQuery, List<LocationStatusInfoDTO>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMaterialSubLotRepository _materialSubLotRepository;

        public GetLocationsByWarehouseIdQueryHandler(IWarehouseRepository warehouseRepository, ILocationRepository locationRepository, IMaterialSubLotRepository materialSubLotRepository)
        {
            _warehouseRepository = warehouseRepository;
            _locationRepository = locationRepository;
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<List<LocationStatusInfoDTO>> Handle(GetLocationsByWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            if (await _warehouseRepository.ExistsAsync(request.WarehouseId) is false)
                throw new EntityNotFoundException(nameof(Warehouse), request.WarehouseId);

            var locations = await _locationRepository.GetLocationsByWarehouseId(request.WarehouseId)
                         ?? throw new EntityNotFoundException(nameof(Location), request.WarehouseId);

            var inforInLocationDTOs = new List<LocationStatusInfoDTO>();
            foreach (var location in locations)
            {
                if (location.TryCalculateLocationMaxVolume(out double maxVolume) is false)
                    continue;

                string locationStorageStatus = "Trống";
                var locationSubLotInfors = new List<LocationSubLotInfo>();

                if (location.HasMaterialSubLots())
                {
                    double locationUsedVolume = 0.0;
                    foreach (var materialSubLot in location.MaterialSubLots)
                    {
                        if (materialSubLot.HasExistingQuantity() is false)
                            continue;

                        var material = await _materialSubLotRepository.GetMaterialBySubLotIdAsync(materialSubLot.MaterialSubLotId)
                                    ?? throw new EntityNotFoundException(nameof(Material), materialSubLot.MaterialSubLotId);

                        if (material.TryCalculateUsedVolume(materialSubLot.ExistingQuantity, out var materialSubLotUsedVolume) is false)
                            continue;

                        locationUsedVolume += materialSubLotUsedVolume;

                        // Create a new instance of SubLot Infor
                        locationSubLotInfors.Add(new LocationSubLotInfo(lotnumber: materialSubLot.LotNumber,
                                                                        quantity: materialSubLot.ExistingQuantity));
                    }

                    double storageRate = CalculateLocationStorageRate(locationUsedVolume, maxVolume);
                    locationStorageStatus = GetLocationStorageStatus(storageRate);
                }

                inforInLocationDTOs.Add(new LocationStatusInfoDTO(locationId: location.LocationId,
                                                                  storageStatus: locationStorageStatus,
                                                                  lotInfors: locationSubLotInfors));
            }

            return inforInLocationDTOs;
        }

        private double CalculateLocationStorageRate(double usedVolume, double maxVolume) => maxVolume > 0 ? (usedVolume / maxVolume) * 100 : 0.0;

        private string GetLocationStorageStatus(double storageRate) => storageRate >= 95.0 ? "Đã đầy" : "Đang chứa hàng";
    }
}
