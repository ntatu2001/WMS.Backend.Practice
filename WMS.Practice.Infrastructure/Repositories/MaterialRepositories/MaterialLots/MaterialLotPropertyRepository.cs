namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class MaterialLotPropertyRepository : BaseRepository, IMaterialLotPropertyRepository
    {
        public MaterialLotPropertyRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(MaterialLotProperty materialLotProperty)
        {
            _context.MaterialLotProperties.Add(materialLotProperty);
        }

        public void Delete(MaterialLotProperty materialLotProperty)
        {
            _context.MaterialLotProperties.Remove(materialLotProperty);
        }

        public async Task<List<MaterialLotProperty>> GetAllAsync()
        {
            return await _context.MaterialLotProperties.ToListAsync();
        }

        public async Task<MaterialLotProperty?> GetMaterialLotPropertyById(string propertyId)
        {
            return await _context.MaterialLotProperties
                                 .FirstOrDefaultAsync(mlp => mlp.PropertyId == propertyId);
        }
    }
}
