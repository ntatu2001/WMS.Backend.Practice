namespace WMS.Practice.Application.Queries.StorageQueries.Locations
{
    public class GetStorageInfoByLocationIdQueryHandler : IRequestHandler<GetStorageInfoByLocationIdQuery, LocationStorageInfoDTO>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMaterialSubLotRepository _materialSubLotRepository;

        public GetStorageInfoByLocationIdQueryHandler(IWarehouseRepository warehouseRepository, ILocationRepository locationRepository, IMaterialSubLotRepository materialSubLotRepository)
        {
            _warehouseRepository = warehouseRepository;
            _locationRepository = locationRepository;
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<LocationStorageInfoDTO> Handle(GetStorageInfoByLocationIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationByIdAsync(request.LocationId)
                        ?? throw new EntityNotFoundException(nameof(Location), request.LocationId);

            var warehouse = await _warehouseRepository.GetWarehouseByIdAsync(location.WarehouseId)
                         ?? throw new EntityNotFoundException(nameof(Warehouse), location.WarehouseId);

            // MaxVolume
            if (location.TryCalculateLocationMaxVolume(out double locationMaxVolume) is false)
                throw new InvalidDataException($"Location {location.LocationId} does not have Max Volume");

            double locationStorageRate = 0.0;
            double locationUsedVolume = 0.0;
            string locationStorageStatus = "Trống";
            var locationSubLotInfors = new List<LocationSubLotInfo>();

            if (location.HasMaterialSubLots())
            {
                foreach (var materialSubLot in location.MaterialSubLots)
                {
                    if (materialSubLot.HasExistingQuantity() is false)
                        continue;

                    locationSubLotInfors.Add(new LocationSubLotInfo(lotnumber: materialSubLot.LotNumber,
                                                                    quantity: materialSubLot.ExistingQuantity));

                    var material = await _materialSubLotRepository.GetMaterialBySubLotIdAsync(materialSubLot.MaterialSubLotId)
                                ?? throw new EntityNotFoundException(nameof(MaterialSubLot), materialSubLot.MaterialSubLotId);

                    if (material.TryCalculateUsedVolume(materialSubLot.ExistingQuantity, out double materialSubLotUsedVolume) is false)
                        continue;

                    locationUsedVolume += materialSubLotUsedVolume;
                }

                locationStorageRate = CalculateLocationStorageRate(locationUsedVolume, locationMaxVolume);
                locationStorageStatus = GetLocationStorageStatus(locationStorageRate);
            }

            var inforInLocationDTO = new LocationStorageInfoDTO(warehouseId: warehouse.WarehouseId,
                                                                warehouseName: warehouse.WarehouseName,
                                                                length: location.GetLengthValue(),
                                                                width: location.GetWidthValue(),
                                                                height: location.GetHeightValue(),
                                                                status: locationStorageStatus,
                                                                lotInfors: locationSubLotInfors,
                                                                maxVolume: locationMaxVolume,
                                                                usableVolume: locationUsedVolume,
                                                                storageRate: locationStorageRate);

            return inforInLocationDTO;
        }

        private double CalculateLocationStorageRate(double usedVolume, double maxVolume) => maxVolume > 0 ? (usedVolume / maxVolume) * 100 : 0.0;

        private string GetLocationStorageStatus(double storageRate) => storageRate >= 95.0 ? "Đã đầy" : "Đang chứa hàng";
    }
}
