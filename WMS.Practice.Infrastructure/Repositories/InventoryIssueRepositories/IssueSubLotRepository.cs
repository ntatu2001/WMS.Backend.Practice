namespace WMS.Practice.Infrastructure.Repositories.InventoryIssueRepositories
{
    public class IssueSubLotRepository : BaseRepository, IIssueSubLotRepository
    {
        public IssueSubLotRepository(WMSDbContext context) : base(context)
        {
        }

        public async Task<List<IssueSubLot>> GetAllAsync()
        {
            return await _context.IssueSubLots.ToListAsync();
        }

        public async Task<IssueSubLot?> GetByIdAsync(string IssueSubLotId)
        {
            return await _context.IssueSubLots.FirstOrDefaultAsync(x => x.IssueSubLotId == IssueSubLotId);
        }
    }
}
