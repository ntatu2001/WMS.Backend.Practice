namespace WMS.Practice.Infrastructure.Repositories.PersonRepositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Remove(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<bool> ExistsAsync(string customerId)
        {
            return await _context.Customers.AnyAsync(x => x.CustomerId == customerId);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByCustomerIdAsync(string Id)
        {
            return await _context.Customers
                                 .FirstOrDefaultAsync(c => c.CustomerId == Id);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }
    }
}
