namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetCustomerById(string Id);
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}
