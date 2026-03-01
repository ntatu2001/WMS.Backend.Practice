namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IInventoryIssueRepository : IRepository<InventoryIssue>
    {
        Task<List<InventoryIssue>> GetAllInventoryIssuesAsync();
        Task<InventoryIssue?> GetInventoryIssueByIdAsync(string inventoryIssueId);
        Task<List<InventoryIssue>> GetInventoryIssuesByTimeRangeOption(DateTime start, DateTime end);
        Task<List<InventoryIssue>> GetInventoryIssuesByEntryIds(List<string> entryId);
        IQueryable<InventoryIssue> QueryAllInventoryIssues();
        void Create(InventoryIssue inventoryIssue);
        void Delete(InventoryIssue inventoryIssue);
    }
}
