namespace WMS.Practice.Domain.InterfaceRepositories.IStorage
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<bool> ExistsAsync(string warehouseId);
        Task<List<Warehouse>> GetAllWarehouses();
        Task<Warehouse?> GetWarehouseByIdAsync(string warehouseId);
        Task<string?> GetWarehouseNameByIdAsync(string warehouseId);
        Task<List<string>> GetWarehouseIdByWarehouseNameAsync(string warehouseName);
        Task<(string WarehouseId, string WarehouseName)?> GetWarehouseNameAndIdByIdAsync(string warehouseId);
        void Create(Warehouse warehouse);
        void Delete(Warehouse warehouse);
        void Update(Warehouse warehouse);
    }
}
