namespace WMS.Practice.Domain.InterfaceRepositories.IStorage
{
    public interface IWarehousePropertyRepository : IRepository<WarehouseProperty>
    {
        Task<bool> ExistsAsync(string propertyId);
        Task<WarehouseProperty?> GetById(string propertyId);
        void Create(WarehouseProperty warehouseProperty);
    }
}
