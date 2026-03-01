namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt
{
    public interface IInventoryReceiptRepository : IRepository<InventoryReceipt>
    {
        Task<List<InventoryReceipt>> GetAllInventoryReceiptsAsync();
        Task<InventoryReceipt?> GetInventoryReceiptByReceiptIdAsync(string inventoryReceiptId);
        Task<List<InventoryReceipt>> GetInventoryReceiptsByEntryIds(List<string> entryId);
        Task<List<InventoryReceipt>> GetInventoryReceiptsByTimeRangeOption(DateTime start, DateTime end);
        IQueryable<InventoryReceipt> QueryInventoryReceipts();
        void Create(InventoryReceipt inventoryReceipt);
        void Delete(InventoryReceipt inventoryReceipt);
    }
}
