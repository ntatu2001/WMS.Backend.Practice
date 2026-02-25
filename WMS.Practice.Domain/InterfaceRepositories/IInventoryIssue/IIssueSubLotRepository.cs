namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IIssueSubLotRepository : IRepository<IssueSubLot>
    {
        Task<bool> ExistsAsync(string issueSubLotId);
        Task<List<IssueSubLot>> GetAllAsync();
        Task<IssueSubLot?> GetByIdAsync(string IssueSubLotId);
        Task<List<(DateTime IssueDate, IssueSubLot SubLot)>> GetIssueSubLotsByLocationIdAndTimeRange(string locationId, DateTime start, DateTime end);
    }
}
