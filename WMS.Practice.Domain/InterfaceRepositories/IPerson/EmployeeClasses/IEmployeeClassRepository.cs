namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface IEmployeeClassRepository : IRepository<EmployeeClass>
    {
        Task<List<EmployeeClass>> GetAllAsync();
        Task<EmployeeClass?> GetEmployeeClassByIdAsync(string employeeClassId);
    }
}
