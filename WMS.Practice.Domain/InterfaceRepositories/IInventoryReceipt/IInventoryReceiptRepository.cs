namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt
{
    public interface IInventoryReceiptRepository : IRepository<InventoryReceipt>
    {
        Task<List<InventoryReceipt>> GetAllAsync();
        Task<InventoryReceipt?> GetByReceiptIdAsync(string inventoryReceiptId);
        Task<List<InventoryReceipt>> GetInventoryReceiptsByEntryIds(List<string> entryId);
        Task<List<InventoryReceipt>> GetInventoryReceiptsByTimeRangeOption(DateTime start, DateTime end);
        void Create(InventoryReceipt inventoryReceipt);
        void Delete(InventoryReceipt inventoryReceipt);
    }
}
