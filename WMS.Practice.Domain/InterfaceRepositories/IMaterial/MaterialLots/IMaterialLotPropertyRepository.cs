namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialLotPropertyRepository : IRepository<MaterialLotProperty>
    {
        Task<bool> ExistAsync(string propertyId);
        Task<List<MaterialLotProperty>> GetAllAsync();
        Task<MaterialLotProperty?> GetMaterialLotPropertyById(string id);
        void Create(MaterialLotProperty materialLotProperty);
        void Delete(MaterialLotProperty materialLotProperty);
        void Update(MaterialLotProperty materialLotProperty);
    }
}
