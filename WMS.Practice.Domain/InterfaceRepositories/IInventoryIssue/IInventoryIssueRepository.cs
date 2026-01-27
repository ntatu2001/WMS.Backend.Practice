namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IInventoryIssueRepository : IRepository<InventoryIssue>
    {
        Task<List<InventoryIssue>> GetAllAsync();
        Task<InventoryIssue?> GetByIdAsync(string inventoryIssueId);
        Task<List<InventoryIssue>> GetInventoryIssuesByLocationId(string locationId);
        Task<List<InventoryIssue>> GetInventoryIssuesByTimeRangeOption(DateTime start, DateTime end);
        void Create(InventoryIssue inventoryIssue);
        void Delete(InventoryIssue inventoryIssue);
    }
}
