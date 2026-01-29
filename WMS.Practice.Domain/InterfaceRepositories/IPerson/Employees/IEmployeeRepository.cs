namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetEmployeeByIdAsync(string employeeId);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
