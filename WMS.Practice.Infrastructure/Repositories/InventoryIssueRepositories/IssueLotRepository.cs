namespace WMS.Practice.Infrastructure.Repositories.InventoryIssueRepositories
{
    public class IssueLotRepository : BaseRepository, IIssueLotRepository
    {
        public IssueLotRepository(WMSDbContext context) : base(context)
        {
        }

        public async Task<List<IssueLot>> GetAllIssueLotsAsync()
        {
            return await _context.IssueLots.ToListAsync();
        }

        public async Task<IssueLot?> GetIssueLotWithDetailsByIssueLotIdAsync(string issueLotId)
        {
            return await _context.IssueLots
                                 .Include(x => x.SubLots)
                                    .ThenInclude(x => x.MaterialSubLot)
                                 .Include(x => x.InventoryIssueEntry)
                                    .ThenInclude(x => x.InventoryIssue)
                                 .FirstOrDefaultAsync(x => x.IssueLotId == issueLotId);
        }

        public Task<IssueLot?> GetIssueLotByIssueLotIdAsync(string issueLotId)
        {
            return _context.IssueLots
                           .Include(x => x.SubLots)
                               .ThenInclude(x => x.MaterialSubLot)
                           .FirstOrDefaultAsync(x => x.IssueLotId == issueLotId);
        }

        public async Task<List<IssueLot>> GetIssueLotsNotDone()
        {
            return await _context.IssueLots
                                 .Include(x => x.InventoryIssueEntry)
                                    .ThenInclude(x => x.InventoryIssue)
                                 .Where(x => x.LotStatus != LotStatus.Done)
                                 .ToListAsync();
        }
    }
}
