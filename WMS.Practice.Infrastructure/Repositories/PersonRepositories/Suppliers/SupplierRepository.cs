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

        public void Delete(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public Task<Supplier?> GetByIdAsync(string supplierId)
        {
            return _context.Suppliers
                           .FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }

        public void Update(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
        }
    }
}
