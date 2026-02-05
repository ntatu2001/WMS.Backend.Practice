namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialSubLotRepository : IRepository<MaterialSubLot>
    {
        Task<bool> ExistAsync(string materialSubLotId);
        Task<List<MaterialSubLot>> GetAllAsync();
        Task<List<MaterialSubLot>> GetMaterialSubLotsByLocationId(string locationId);
        Task<MaterialSubLot?> GetByIdAsync(string Id);
        Task<List<MaterialSubLot>> GetMaterialSubLotsByLotNumber(string lotNumber);
        Task<MaterialSubLot> GetMaterialSubLotByLotNumberAndLocationId(string lotNumber, string locationId);
        Task<List<MaterialSubLot>> GetMaterialSubLotsByStatus(LotStatus status);
        void Create(MaterialSubLot materialSubLot);
        void Delete(MaterialSubLot materialSubLot);
        void Update(MaterialSubLot materialSubLot);
    }
}
