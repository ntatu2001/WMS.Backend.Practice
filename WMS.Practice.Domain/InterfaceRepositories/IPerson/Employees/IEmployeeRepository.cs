namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> ExistAsync(string employeeId);
        Task<List<Employee>> GetAllAsync();
        IQueryable<Employee> QueryEmployees();
        Task<List<(string EmployeeId, string EmployeeName)>> GetAllEmployeeNameIdAsync();
        Task<Employee?> GetEmployeeByIdAsync(string employeeId);
        Task<string?> GetEmployeeNameByIdAsync(string employeeId);
        Task<(string EmployeeId, string EmployeeName)?> GetEmployeeIdAndNameByIdAsync(string employeeId);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
