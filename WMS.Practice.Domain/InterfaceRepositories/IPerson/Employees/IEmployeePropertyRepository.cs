namespace WMS.Practice.Domain.InterfaceRepositories.IPerson
{
    public interface IEmployeePropertyRepository : IRepository<EmployeeProperty>
    {
        Task<EmployeeProperty?> GetByIdAsync(string propertyId);
        Task<List<EmployeeProperty>> GetAllEmployeeProperties();
        void Create(EmployeeProperty employeeProperty);
        void Delete(EmployeeProperty employeeProperty);
        void Update(EmployeeProperty employeeProperty);
    }
}
