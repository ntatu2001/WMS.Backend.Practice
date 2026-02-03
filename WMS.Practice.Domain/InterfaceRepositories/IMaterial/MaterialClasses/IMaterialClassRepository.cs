namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialClassRepository : IRepository<MaterialClass>
    {
        Task<bool> ExistsAsync(string classId);
        Task<List<MaterialClass>> GetAllAsync();
        Task<MaterialClass?> GetByClassIdAsync(string id);
        Task<List<MaterialClass>> GetByClassIdsAsync(List<string> ids);
        void Create(MaterialClass materialClass);
        void Delete(MaterialClass materialClass);
        void Update(MaterialClass materialClass);
    }
}
