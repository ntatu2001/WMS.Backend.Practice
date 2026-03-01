namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<bool> ExistsAsync(string supplierId);
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetSupplierByIdAsync(string supplierId);
        Task<string?> GetSupplierNameByIdAsync(string supplierId);
        Task<(string SupplierId, string SupplierName)?> GetSupplierNameAndIdByIdAsync(string supplierId);   
        void Create(Supplier supplier);
        void Remove(Supplier supplier);
        void Update(Supplier supplier);
    }
}
