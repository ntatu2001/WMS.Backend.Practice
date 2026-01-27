namespace WMS.Practice.Domain.InterfaceRepositories.IStorage
{
    public interface IWarehousePropertyRepository : IRepository<WarehouseProperty>
    {
        Task<WarehouseProperty?> GetById(string Id);
        void Create(WarehouseProperty warehouseProperty);
    }
}
