namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt
{
    public interface IInventoryReceiptEntryRepository : IRepository<InventoryReceiptEntry>
    {
        Task<List<InventoryReceiptEntry>> GetAllAsync();
        Task<InventoryReceiptEntry?> GetById(string inventoryReceiptEntryId);
        Task<InventoryReceiptEntry?> GetByReceiptLotId(string receiptLotId);
        void Delete(InventoryReceiptEntry inventoryReceiptEntry);
    }
}
