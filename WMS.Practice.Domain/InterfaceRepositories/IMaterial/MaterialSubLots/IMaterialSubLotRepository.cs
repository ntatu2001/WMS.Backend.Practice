namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialSubLotRepository : IRepository<MaterialSubLot>
    {
        Task<bool> ExistsAsync(string materialSubLotId);
        Task<bool> ExistMaterialSubLotsByLotNumber(string lotNumber);
        Task<List<MaterialSubLot>> GetAllMaterialSubLotsAsync();
        Task<List<MaterialSubLot>> GetMaterialSubLotsByLocationIdAsync(string locationId);
        Task<MaterialSubLot?> GetMaterialSubLotByIdAsync(string Id);
        Task<List<MaterialSubLot>> GetMaterialSubLotsByLotNumberAsync(string lotNumber);
        Task<MaterialSubLot> GetMaterialSubLotByLotNumberAndLocationId(string lotNumber, string locationId);
        Task<List<MaterialSubLot>> GetMaterialSubLotsByStatusAsync(LotStatus status);
        Task<Material?> GetMaterialBySubLotIdAsync(string subLotId);
        void Create(MaterialSubLot materialSubLot);
        void Delete(MaterialSubLot materialSubLot);
        void Update(MaterialSubLot materialSubLot);
    }
}
