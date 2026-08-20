namespace WMS.Practice.Application.Services.Locations
{
    public interface ILocationCapacityService
    {
        Task<(double usedVolume, double maxVolume)> CalculateLocationVolumeAsync(Location location, string? excludeSubLotId = null);
    }
}
