namespace WMS.Practice.Infrastructure.Repositories.InventoryIssueRepositories
{
    public class InventoryIssueRepository : BaseRepository, IInventoryIssueRepository
    {
        public InventoryIssueRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(InventoryIssue inventoryIssue)
        {
            _context.InventoryIssues.Add(inventoryIssue);
        }

        public void Delete(InventoryIssue inventoryIssue)
        {
            _context.InventoryIssues.Remove(inventoryIssue);
        }

        public async Task<List<InventoryIssue>> GetAllAsync()
        {
            return await _context.InventoryIssues
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.IssueLot)
                                        .ThenInclude(e => e.SubLots)
                                            .ThenInclude(e => e.MaterialSubLot)
                                 .ToListAsync(); 
        }

        public async Task<InventoryIssue?> GetByIdAsync(string inventoryIssueId)
        {
            return await _context.InventoryIssues
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.IssueLot)
                                        .ThenInclude(e => e.SubLots)
                                            .ThenInclude(e => e.MaterialSubLot)
                                 .FirstOrDefaultAsync(ii => ii.InventoryIssueId == inventoryIssueId);
        }

        public async Task<List<InventoryIssue>> GetInventoryIssuesByLocationId(string locationId)
        {
            return await _context.InventoryIssues
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.IssueLot)
                                        .ThenInclude(e => e.SubLots)
                                            .ThenInclude(e => e.MaterialSubLot)
                                 .Where(ii => ii.Entries.Any(entry => entry.IssueLot.SubLots.Any(sub => sub.MaterialSubLot.LocationId == locationId)))
                                 .ToListAsync();
        }

        public async Task<List<InventoryIssue>> GetInventoryIssuesByTimeRangeOption(DateTime start, DateTime end)
        {
            return await _context.InventoryIssues
                                 .Include(e => e.Entries)
                                    .ThenInclude(e => e.IssueLot)
                                        .ThenInclude(e => e.SubLots)
                                            .ThenInclude(e => e.MaterialSubLot)
                                 .Where(ii => ii.IssueDate >= start && ii.IssueDate <= end)
                                 .ToListAsync();
        }

        public Task<List<InventoryIssue>> GetInventoryIssuesByEntryIds(List<string> entryIds)
        {
            return _context.InventoryIssues
                           .Include(e => e.Entries)
                              .ThenInclude(e => e.IssueLot)
                                  .ThenInclude(e => e.SubLots)
                                      .ThenInclude(e => e.MaterialSubLot)
                           .Where(ii => ii.Entries.Any(entry => entryIds.Contains(entry.InventoryIssueEntryId)))
                           .ToListAsync();
        }
    }
}
