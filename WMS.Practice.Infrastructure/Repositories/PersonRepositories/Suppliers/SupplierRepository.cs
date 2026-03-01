namespace WMS.Practice.Infrastructure.Repositories.PersonRepositories
{
    public class SupplierRepository : BaseRepository, ISupplierRepository
    {
        public SupplierRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
        }

        public void Remove(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
        }

        public async Task<bool> ExistsAsync(string supplierId)
        {
            return await _context.Suppliers.AnyAsync(x => x.SupplierId == supplierId);
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public Task<Supplier?> GetSupplierByIdAsync(string supplierId)
        {
            return _context.Suppliers
                           .FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }

        public void Update(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
        }

        public async Task<string?> GetSupplierNameByIdAsync(string supplierId)
        {
            return await _context.Suppliers.Where(s => s.SupplierId == supplierId)
                                  .Select(s => s.SupplierName)
                                  .FirstOrDefaultAsync();
        }

        public async Task<(string SupplierId, string SupplierName)?> GetSupplierNameAndIdByIdAsync(string supplierId)
        {
            var item = await _context.Suppliers.Where(s => s.SupplierId == supplierId)
                                  .Select(s => new { s.SupplierId, s.SupplierName })
                                  .FirstOrDefaultAsync();

            return item is not null ? (item.SupplierId, item.SupplierName) : null;
        }
    }
}
