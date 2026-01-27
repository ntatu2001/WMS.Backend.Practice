namespace WMS.Practice.Domain.InterfaceRepositories.IStorage
{
    public interface ILocationPropertyRepository : IRepository<LocationProperty>
    {
        void Create(LocationProperty locationProperty);
        void Remove(LocationProperty locationProperty);
        Task<List<LocationProperty>> GetAllLocationProperties();
        Task<LocationProperty?> GetLocationPropertyById(string propertyId);
        Task<List<LocationProperty>> GetLocationPropertiesByLocationId(string locationId);
    }
}
