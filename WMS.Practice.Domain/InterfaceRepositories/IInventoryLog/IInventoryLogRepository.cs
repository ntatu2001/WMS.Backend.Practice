namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryLog
{
    public interface IInventoryLogRepository : IRepository<InventoryLog>
    {
        Task<List<InventoryLog>> GetInventoryLogByLotNumberAndStatus(string lotNumber, string status);
        Task<List<InventoryLog>> GetAllInventoryLogs(string transactionType);
        Task<List<InventoryLog>> GetAllInventoryLogsByTime(string transactionType, DateTime dateTime);
        void Create(InventoryLog inventoryLog);
    }
}
