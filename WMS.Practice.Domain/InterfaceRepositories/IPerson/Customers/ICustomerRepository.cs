namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> ExistsAsync(string customerId);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetCustomerByIdAsync(string Id);
        void Create(Customer customer);
        void Update(Customer customer);
        void Remove(Customer customer);
    }
}
