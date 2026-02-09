namespace WMS.Practice.Application.Services.InventoryLogs.InventoryReceipts
{
    public interface IReceiptLoggingService
    {
        Task UpdateInventoryLog(InventoryReceipt newInventoryReceipt);
    }
}
