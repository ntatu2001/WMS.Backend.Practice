namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class MaterialPropertyRepository : BaseRepository, IMaterialPropertyRepository
    {
        public MaterialPropertyRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(MaterialProperty materialProperty)
        {
            _context.MaterialProperties.Add(materialProperty);
        }

        public void Delete(MaterialProperty materialProperty)
        {
            _context.MaterialProperties.Remove(materialProperty);
        }

        public async Task<List<MaterialProperty>> GetAllAsync()
        {
            return await _context.MaterialProperties.ToListAsync();
        }

        public Task<MaterialProperty?> GetByPropertyIdAsync(string materialPropertyId)
        {
            return _context.MaterialProperties
                           .FirstOrDefaultAsync(mp => mp.PropertyId.Equals(materialPropertyId, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(MaterialProperty materialProperty)
        {
            _context.MaterialProperties.Update(materialProperty);
        }
    }
}
