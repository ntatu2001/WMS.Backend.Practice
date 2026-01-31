namespace WMS.Practice.Infrastructure.Repositories.StorageRepositories
{
    public class WarehousePropertyRepository : BaseRepository, IWarehousePropertyRepository
    {
        public WarehousePropertyRepository(WMSDbContext dbContext) : base(dbContext)
        {
        }

        public void Create(WarehouseProperty warehouseProperty)
        {
            _context.WarehouseProperties.Add(warehouseProperty);
        }

        public async Task<bool> ExistsAsync(string propertyId)
        {
            return await _context.WarehouseProperties.AnyAsync(x => x.PropertyId == propertyId);
        }

        public async Task<WarehouseProperty?> GetById(string propertyId)
        {
            return await _context.WarehouseProperties.FirstOrDefaultAsync(wp => wp.PropertyId.Equals(propertyId, StringComparison.OrdinalIgnoreCase));
        }
    }
}
