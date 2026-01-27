namespace WMS.Practice.Infrastructure.Repositories.StorageRepositories
{
    public class LocationPropertyRepository : BaseRepository, ILocationPropertyRepository
    {
        public LocationPropertyRepository(WMSDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<LocationProperty>> GetAllLocationProperties()
        {
            return await _context.LocationsProperties.ToListAsync();
        }

        public async Task<List<LocationProperty>> GetLocationPropertiesByLocationId(string locationId)
        {
            return await _context.LocationsProperties
                                 .Where(lp => lp.LocationId == locationId)
                                 .ToListAsync();
        }

        public async Task<LocationProperty?> GetLocationPropertyById(string propertyId)
        {
            return await _context.LocationsProperties
                                 .FirstOrDefaultAsync(lp => lp.PropertyId == propertyId);
        }

        public void Create(LocationProperty locationProperty)
        {
            _context.LocationsProperties.Add(locationProperty);
        }

        public void Remove(LocationProperty locationProperty)
        {
            _context.LocationsProperties.Remove(locationProperty);
        }
    }
}
