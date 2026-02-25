namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IInventoryIssueRepository : IRepository<InventoryIssue>
    {
        Task<List<InventoryIssue>> GetAllAsync();
        Task<InventoryIssue?> GetByIdAsync(string inventoryIssueId);
        Task<List<InventoryIssue>> GetInventoryIssuesByTimeRangeOption(DateTime start, DateTime end);
        Task<List<InventoryIssue>> GetInventoryIssuesByEntryIds(List<string> entryId);
        void Create(InventoryIssue inventoryIssue);
        void Delete(InventoryIssue inventoryIssue);
    }
}
