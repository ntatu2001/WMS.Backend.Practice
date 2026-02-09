namespace WMS.Practice.Application.Services.InventoryLogs.InventoryIssues
{
    public interface IIssueLoggingService
    {
        Task UpdateInventoryLog(InventoryIssue issueLot);
    }
}
