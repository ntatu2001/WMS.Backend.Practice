namespace WMS.Practice.Domain.InterfaceRepositories.IStorage
{
    public interface ILocationPropertyRepository : IRepository<LocationProperty>
    {
        void Create(LocationProperty locationProperty);
        void Remove(LocationProperty locationProperty);
        Task<bool> ExistsAsync(string propertyId);
        Task<List<LocationProperty>> GetAllLocationProperties();
        Task<LocationProperty?> GetLocationPropertyByIdAsync(string propertyId);
        Task<List<LocationProperty>> GetLocationPropertiesByLocationId(string locationId);
    }
}
