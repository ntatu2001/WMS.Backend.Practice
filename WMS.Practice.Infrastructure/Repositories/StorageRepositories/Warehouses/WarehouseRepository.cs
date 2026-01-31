namespace WMS.Practice.Infrastructure.Repositories.StorageRepositories
{
    public class WarehouseRepository : BaseRepository, IWarehouseRepository
    {
        public WarehouseRepository(WMSDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Warehouse>> GetAllWarehouses()
        {
            return await _context.Warehouses
                                 .Include(l => l.Locations)
                                 .Include(p => p.Properties)
                                 .ToListAsync();
        }

        public async Task<Warehouse?> GetWarehouseByIdAsync(string id)
        {
            return await _context.Warehouses
                                 .Include(l => l.Locations)
                                 .Include(p => p.Properties)
                                 .FirstOrDefaultAsync(w => w.WarehouseId == id);
        }

        public async Task<List<string>> GetWarehouseIdByWarehouseNameAsync(string warehouseName)
        {
            return await _context.Warehouses
                                 .Where(x => x.WarehouseName.Equals(warehouseName, StringComparison.OrdinalIgnoreCase))
                                 .Select(x => x.WarehouseId)
                                 .ToListAsync();
        }

        public void Create(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
        }

        public void Delete(Warehouse warehouse)
        {
            _context.Warehouses.Remove(warehouse);
        }

        public void Update(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
        }

        public async Task<bool> ExistsAsync(string warehouseId)
        {
            return await _context.Warehouses.AnyAsync(x => x.WarehouseId.Equals(warehouseId, StringComparison.OrdinalIgnoreCase));
        }
    }
}
