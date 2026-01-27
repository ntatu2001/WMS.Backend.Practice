namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialClassRepository : IRepository<MaterialClass>
    {
        Task<List<MaterialClass>> GetAllAsync();
        Task<MaterialClass?> GetByIdAsync(string id);
        Task<List<MaterialClass>> GetByIdsAsync(List<string> ids);
        void Create(MaterialClass materialClass);
        void Delete(MaterialClass materialClass);
        void Update(MaterialClass materialClass);
    }
}
