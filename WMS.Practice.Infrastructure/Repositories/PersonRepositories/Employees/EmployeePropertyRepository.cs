namespace WMS.Practice.Infrastructure.Repositories.PersonRepositories
{
    public class EmployeePropertyRepository : BaseRepository, IEmployeePropertyRepository
    {
        public EmployeePropertyRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(EmployeeProperty personProperty)
        {
            _context.EmployeeProperties.Add(personProperty);
        }

        public void Delete(EmployeeProperty personProperty)
        {
            _context.EmployeeProperties.Remove(personProperty);
        }

        public async Task<List<EmployeeProperty>> GetAllEmployeeProperties()
        {
            return await _context.EmployeeProperties.ToListAsync();
        }

        public async Task<EmployeeProperty?> GetByIdAsync(string propertyId)
        {
            return await _context.EmployeeProperties
                                 .FirstOrDefaultAsync(pp => pp.PropertyId == propertyId);
        }

        public void Update(EmployeeProperty employeeProperty)
        {
            _context.EmployeeProperties.Update(employeeProperty);
        }
    }
}
