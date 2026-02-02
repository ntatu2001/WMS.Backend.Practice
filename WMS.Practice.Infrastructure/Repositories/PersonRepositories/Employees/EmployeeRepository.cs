namespace WMS.Practice.Infrastructure.Repositories.PersonRepositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(WMSDbContext context) : base(context)
        {
        }
        public void Create(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public async Task<bool> ExistAsync(string employeeId)
        {
            return await _context.Employees.AnyAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees
                                 .Include(x => x.Properties)
                                 .ToListAsync();
        }
        public async Task<Employee?> GetEmployeeByIdAsync(string employeeId)
        {
            return await _context.Employees
                           .Include(e => e.Properties)
                           .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}
