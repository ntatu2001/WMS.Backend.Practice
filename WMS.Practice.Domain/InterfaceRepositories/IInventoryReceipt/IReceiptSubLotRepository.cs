namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt
{
    public interface IReceiptSubLotRepository : IRepository<ReceiptSubLot>
    {
        Task<List<ReceiptSubLot>> GetAllAsync();
        Task<ReceiptSubLot?> GetByIdAsync(string receiptSubLotId);
        Task<List<ReceiptSubLot>> GetSubLotsByLotId(string lotId);
    }
}
