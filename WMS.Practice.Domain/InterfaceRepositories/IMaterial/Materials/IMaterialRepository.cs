namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<List<Material>> GetAllAsync();
        Task<List<Material>> GetByClassIdAsync(string classId);
        Task<Material?> GetByMaterialIdAsync(string materialId);
        void Create(Material material);
        void Delete(Material material);
        void Update(Material material);
    }
}
