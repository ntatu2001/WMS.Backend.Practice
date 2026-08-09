namespace WMS.Practice.Infrastructure.Repositories.PersonRepositories
{
    public class EmployeeClassRepository : BaseRepository, IEmployeeClassRepository
    {
        public EmployeeClassRepository(WMSDbContext context) : base(context)
        {
        }

        public async Task<List<EmployeeClass>> GetAllAsync()
        {
            return await _context.EmployeeClasses
                                 .Include(ec => ec.Properties)
                                 .ToListAsync();
        }

        public async Task<EmployeeClass?> GetEmployeeClassByIdAsync(string employeeClassId)
        {
            return await _context.EmployeeClasses
                                 .Include(ec => ec.Properties)
                                 .FirstOrDefaultAsync(ec => ec.EmployeeClassId == employeeClassId);
        }
    }
}
