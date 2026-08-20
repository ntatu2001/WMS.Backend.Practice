namespace WMS.Practice.Application.Services.Locations
{
    public class LocationCapacityService : ILocationCapacityService
    {
        private readonly IMaterialSubLotRepository _materialSubLotRepository;

        public LocationCapacityService(IMaterialSubLotRepository materialSubLotRepository)
        {
            _materialSubLotRepository = materialSubLotRepository;
        }

        public async Task<(double usedVolume, double maxVolume)> CalculateLocationVolumeAsync(Location location, string? excludeSubLotId = null)
        {
            if (location.TryCalculateLocationMaxVolume(out double maxVolume) is false)
                throw new InvalidOperationException($"Location {location.LocationId} does not have Length/Width/Height configured, cannot calculate capacity");

            double usedVolume = 0.0;
            if (location.HasMaterialSubLots())
            {
                foreach (var materialSubLot in location.MaterialSubLots)
                {
                    if (materialSubLot.MaterialSubLotId == excludeSubLotId)
                        continue;

                    if (materialSubLot.HasExistingQuantity() is false)
                        continue;

                    var material = await _materialSubLotRepository.GetMaterialBySubLotIdAsync(materialSubLot.MaterialSubLotId)
                                ?? throw new EntityNotFoundException(nameof(Material), materialSubLot.MaterialSubLotId);

                    if (material.TryCalculateUsedVolume(materialSubLot.ExistingQuantity, out double materialSubLotUsedVolume) is false)
                        continue;

                    usedVolume += materialSubLotUsedVolume;
                }
            }

            return (usedVolume, maxVolume);
        }
    }
}
