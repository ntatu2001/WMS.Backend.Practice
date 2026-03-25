namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt
{
    public interface IReceiptSubLotRepository : IRepository<ReceiptSubLot>
    {
        Task<List<ReceiptSubLot>> GetAllReceiptSubLotsAsync();
        Task<ReceiptSubLot?> GetReceiptSubLotByIdAsync(string receiptSubLotId);
        Task<List<ReceiptSubLot>> GetSubLotsByLotId(string lotId);
        Task<string?> GetMaterialNameByReceiptSubLotIdAsync(string receiptSubLotId);
        Task<List<(DateTime ReceiptDate, ReceiptSubLot SubLot)>> GetReceiptSubLotsByLocationIdAndTimeRange(string locationId, DateTime start, DateTime end);
    }
}
