namespace WMS.Practice.Infrastructure.Repositories
{
    public class BaseRepository
    {
        public WMSDbContext _context { get; }
        public IUnitOfWork UnitOfWork => _context;

        public BaseRepository(WMSDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
