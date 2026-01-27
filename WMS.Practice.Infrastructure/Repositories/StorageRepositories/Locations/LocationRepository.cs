namespace WMS.Practice.Infrastructure.Repositories.StorageRepositories
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        public LocationRepository(WMSDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Location>> GetAllLocations()
        {
            return await _context.Locations
                                 .Include(l => l.Properties)
                                 .Include(l => l.MaterialSubLots)
                                 .ToListAsync();
        }

        public async Task<Location?> GetLocationByIdAsync(string locationId)
        {
            return await _context.Locations
                                 .Include(l => l.Properties)
                                 .Include(l => l.MaterialSubLots)
                                 .FirstOrDefaultAsync(x => x.LocationCode.Equals(locationId, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<List<Location>> GetLocationsByWarehouseId(string warehouseId)
        {
            return await _context.Locations
                                 .Include(l => l.Properties)
                                 .Include(l => l.MaterialSubLots)
                                 .Where(x => x.WarehouseId.Equals(warehouseId, StringComparison.OrdinalIgnoreCase))
                                 .ToListAsync();
        }

        public void Create(Location location)
        {
            _context.Locations.Add(location);
        }

        public void Update(Location location)
        {
            _context.Locations.Update(location);
        }

        public void Delete(Location location)
        {
            _context.Locations.Remove(location);
        }
    }
}
