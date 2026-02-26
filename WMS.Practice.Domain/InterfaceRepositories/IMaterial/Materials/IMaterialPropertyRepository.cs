namespace WMS.Practice.Domain.InterfaceRepositories.IMaterial
{
    public interface IMaterialPropertyRepository : IRepository<MaterialProperty>
    {
        Task<List<MaterialProperty>> GetAllMaterialPropertiesAsync();
        Task<MaterialProperty?> GetMaterialPropertyByPropertyIdAsync(string materialPropertyId);
        void Create(MaterialProperty materialProperty);
        void Delete(MaterialProperty materialProperty);
        void Update(MaterialProperty materialProperty);
        Task<bool> ExistAsync(string propertyId);
    }
}
