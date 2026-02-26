namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialClassPropertyRepository : IRepository<MaterialClassProperty>
    {
        Task<List<MaterialClassProperty>> GetAllAsync();
        Task<MaterialClassProperty?> GetMaterialClassPropertyByPropertyIdAsync(string id);
        void Create(MaterialClassProperty materialClassProperty);
        void Delete(MaterialClassProperty materialClassProperty);
        void Update(MaterialClassProperty materialClassProperty);
    }
}
