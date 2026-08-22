namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface IEmployeeClassRepository : IRepository<EmployeeClass>
    {
        Task<List<EmployeeClass>> GetAllAsync();
        Task<List<(string EmployeeClassId, string EmployeeClassName)>> GetAllEmployeeClassNameIdAsync();
        Task<EmployeeClass?> GetEmployeeClassByIdAsync(string employeeClassId);
    }
}
