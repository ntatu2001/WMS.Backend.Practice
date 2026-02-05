namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialLotRepository : IRepository<MaterialLot>
    {
        Task<bool> ExistAsync(string lotNumber);
        Task<List<MaterialLot>> GetAllAsync();
        Task<MaterialLot?> GetMaterialLotByIdAsync(string lotNumber);
        Task<MaterialLot?> GetMaterialLotWithIssuesByIdAsync(string lotNumber);
        Task<List<MaterialLot>> GetMaterialLotsByMaterialId(string materialId);
        Task<List<MaterialLot>> GetMaterialLotsByStatus(LotStatus status);
        void Create(MaterialLot materialLot);
        void Delete(MaterialLot materialLot);
        void Update(MaterialLot materialLot);
    }
}
