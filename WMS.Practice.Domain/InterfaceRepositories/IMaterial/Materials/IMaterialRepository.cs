namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<bool> ExistAsync(string materialId);
        Task<List<Material>> GetAllMaterialsAsync();
        Task<List<Material>> GetMaterialsByClassIdAsync(string classId);
        Task<List<Material>> GetMaterialsByClassIdAndMaterialLots(string classId);
        Task<Material?> GetMaterialByIdAsync(string materialId);
        void Create(Material material);
        void Delete(Material material);
        void Update(Material material);
    }
}
