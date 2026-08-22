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

        public async Task<List<(string EmployeeClassId, string EmployeeClassName)>> GetAllEmployeeClassNameIdAsync()
        {
            var items = await _context.EmployeeClasses
                                 .Select(ec => new { ec.EmployeeClassId, ec.EmployeeClassName })
                                 .ToListAsync();

            return items.Select(ec => (ec.EmployeeClassId, ec.EmployeeClassName)).ToList();
        }

        public async Task<EmployeeClass?> GetEmployeeClassByIdAsync(string employeeClassId)
        {
            return await _context.EmployeeClasses
                                 .Include(ec => ec.Properties)
                                 .FirstOrDefaultAsync(ec => ec.EmployeeClassId == employeeClassId);
        }
    }
}
