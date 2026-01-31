namespace WMS.Practice.Domain.InterfaceRepositories.IStorage
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<bool> ExistsAsync(string warehouseId);
        Task<List<Warehouse>> GetAllWarehouses();
        Task<Warehouse?> GetWarehouseByIdAsync(string id);
        Task<List<string>> GetWarehouseIdByWarehouseNameAsync(string warehouseName);
        void Create(Warehouse warehouse);
        void Delete(Warehouse warehouse);
        void Update(Warehouse warehouse);
    }
}
