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

        public async Task<WarehouseProperty?> GetById(string Id)
        {
            return await _context.WarehouseProperties.FirstOrDefaultAsync(wp => wp.PropertyId.Equals(Id, StringComparison.OrdinalIgnoreCase));
        }
    }
}
