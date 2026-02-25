namespace WMS.Practice.Infrastructure.Repositories.InventoryIssueRepositories
{
    public class IssueSubLotRepository : BaseRepository, IIssueSubLotRepository
    {
        public IssueSubLotRepository(WMSDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(string issueSubLotId)
        {
            return await _context.IssueSubLots.AnyAsync(x => x.IssueSubLotId == issueSubLotId);
        }

        public async Task<List<IssueSubLot>> GetAllAsync()
        {
            return await _context.IssueSubLots.ToListAsync();
        }

        public async Task<IssueSubLot?> GetByIdAsync(string IssueSubLotId)
        {
            return await _context.IssueSubLots.FirstOrDefaultAsync(x => x.IssueSubLotId == IssueSubLotId);
        }

        public async Task<List<(DateTime IssueDate, IssueSubLot SubLot)>> GetIssueSubLotsByLocationIdAndTimeRange(string locationId, DateTime start, DateTime end)
        {
            var data = await _context.InventoryIssues.Where(receipt => receipt.IssueDate >= start && receipt.IssueDate <= end)
                                                        .SelectMany(receipt => receipt.Entries.Where(e => e.IssueLot.LotStatus == LotStatus.Done)
                                                            .SelectMany(e => e.IssueLot.IssueSubLots.Where(s => s.MaterialSubLot.LocationId == locationId)
                                                                .Select(subLot => new { receipt.IssueDate, SubLot = subLot })))
                                                        .ToListAsync();

            return data.Select(x => (x.IssueDate, x.SubLot)).ToList();
        }
    }
}
