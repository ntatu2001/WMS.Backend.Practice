namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt
{
    public interface IReceiptLotRepository : IRepository<ReceiptLot>
    {
        Task<bool> ExistAsync(string receiptLotId);
        Task<List<ReceiptLot>> GetAllAsync();
        Task<List<ReceiptLot>> GetReceiptLotsAsPending();
        Task<ReceiptLot?> GetById(string receiptLotId);
        Task<ReceiptLot?> GetReceiptByLotId(string receiptLotId);
        Task<ReceiptLot?> GetByIdAsync(string receiptLotId);
    }
}
