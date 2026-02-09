namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<bool> ExistsAsync(string supplierId);
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetSupplierByIdAsync(string id);
        void Create(Supplier supplier);
        void Remove(Supplier supplier);
        void Update(Supplier supplier);
    }
}
