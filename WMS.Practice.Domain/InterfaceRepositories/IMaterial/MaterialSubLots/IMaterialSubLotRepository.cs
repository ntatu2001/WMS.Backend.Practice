namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialSubLotRepository : IRepository<MaterialSubLot>
    {
        Task<List<MaterialSubLot>> GetAllAsync();
        Task<List<MaterialSubLot>> GetMaterialSubLotsByLocationId(string locationId);
        Task<MaterialSubLot?> GetByIdAsync(string Id);
        Task<List<MaterialSubLot>> GetMaterialSubLotsByLotNumber(string lotNumber);
        Task<List<MaterialSubLot>> GetMaterialSubLotsByStatus(LotStatus status);
        void Create(MaterialSubLot materialSubLot);
        void Delete(MaterialSubLot materialSubLot);
        void Update(MaterialSubLot materialSubLot);
    }
}
