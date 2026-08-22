namespace WMS.Practice.Domain.InterfaceRepositories.IInventoryIssue
{
    public interface IInventoryIssueEntryRepository : IRepository<InventoryIssueEntry>
    {
        Task<List<InventoryIssueEntry>> GetAllInventoryIssueEntriesAsync();
        IQueryable<InventoryIssueEntry> QueryInventoryIssueEntries();
        Task<InventoryIssueEntry?> GetInventoryIssueEntryByIdAsync(string InventoryIssueEntryId);
        void Delete(InventoryIssueEntry inventoryIssueEntry);
    }
}
