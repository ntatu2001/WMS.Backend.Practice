namespace WMS.Practice.Infrastructure.Repositories.InventoryIssueRepositories
{
    public class InventoryIssueEntryRepository : BaseRepository, IInventoryIssueEntryRepository
    {
        public InventoryIssueEntryRepository(WMSDbContext context) : base(context)
        {
        }

        public void Delete(InventoryIssueEntry inventoryIssueEntry)
        {
            _context.InventoryIssueEntries.Remove(inventoryIssueEntry);
        }

        public async Task<List<InventoryIssueEntry>> GetAllInventoryIssueEntriesAsync()
        {
            return await _context.InventoryIssueEntries
                                 .Include(e => e.InventoryIssue)
                                 .ToListAsync();
        }

        public async Task<InventoryIssueEntry?> GetInventoryIssueEntryByIdAsync(string InventoryIssueEntryId)
        {
            return await _context.InventoryIssueEntries
                                 .Include(e => e.InventoryIssue)
                                 .Include(e => e.IssueLot)
                                    .ThenInclude(e => e.IssueSubLots)
                                        .ThenInclude(e => e.MaterialSubLot)
                                 .FirstOrDefaultAsync(e => e.InventoryIssueEntryId == InventoryIssueEntryId);
        }
    }
}
