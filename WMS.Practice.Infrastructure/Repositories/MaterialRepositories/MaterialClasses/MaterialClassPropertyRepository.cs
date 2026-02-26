namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class MaterialClassPropertyRepository : BaseRepository, IMaterialClassPropertyRepository
    {
        public MaterialClassPropertyRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(MaterialClassProperty materialClassProperty)
        {
            _context.MaterialClassProperties.Add(materialClassProperty);
        }

        public void Delete(MaterialClassProperty materialClassProperty)
        {
            _context.MaterialClassProperties.Remove(materialClassProperty);
        }

        public async Task<List<MaterialClassProperty>> GetAllAsync()
        {
            return await _context.MaterialClassProperties.ToListAsync();
        }

        public async Task<MaterialClassProperty?> GetMaterialClassPropertyByPropertyIdAsync(string propertyId)
        {
            return await _context.MaterialClassProperties
                                 .FirstOrDefaultAsync(mcp => mcp.PropertyId == propertyId);
        }

        public void Update(MaterialClassProperty materialClassProperty)
        {
            _context.MaterialClassProperties.Update(materialClassProperty);
        }
    }
}
