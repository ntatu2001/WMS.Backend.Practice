namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetEmployeeById(string id);
        void Create(Employee person);
        void Update(Employee person);
        void Delete(Employee person);
    }
}
