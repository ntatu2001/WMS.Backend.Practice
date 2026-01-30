namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryLog
{
    public interface IInventoryLogRepository : IRepository<InventoryLog>
    {
        Task<List<InventoryLog>> GetInventoryLogByLotNumberAndStatus(string lotNumber, LotStatus status);
        Task<List<InventoryLog>> GetAllInventoryLogs(TransactionType transactionType);
        Task<List<InventoryLog>> GetAllInventoryLogsByTime(TransactionType transactionType, DateTime dateTime);
        void Create(InventoryLog inventoryLog);
    }
}
