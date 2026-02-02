namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> ExistsAsync(string customerId);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetCustomerByCustomerIdAsync(string Id);
        void Create(Customer customer);
        void Update(Customer customer);
        void Remove(Customer customer);
    }
}
