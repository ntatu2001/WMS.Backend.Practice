namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryReceipt
{
    public interface IReceiptLotRepository : IRepository<ReceiptLot>
    {
        Task<List<ReceiptLot>> GetAllAsync();
        Task<List<ReceiptLot>> GetReceiptLotsByStatus();
        Task<ReceiptLot?> GetById(string Id);
        Task<ReceiptLot?> GetReceiptByLotId(string Id);
        Task<ReceiptLot?> GetByIdAsync(string Id);
    }
}
