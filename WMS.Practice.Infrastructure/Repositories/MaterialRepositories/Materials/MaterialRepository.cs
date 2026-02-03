namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class MaterialRepository : BaseRepository, IMaterialRepository
    {
        public MaterialRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(Material material)
        {
            _context.Materials.Add(material);
        }

        public void Delete(Material material)
        {
            _context.Materials.Remove(material);
        }

        public async Task<bool> ExistAsync(string materialId)
        {
            return await _context.Materials.AnyAsync(x => x.MaterialId == materialId);
        }

        public async Task<List<Material>> GetAllAsync()
        {
            return await _context.Materials
                                 .Include(e => e.Properties)
                                 .ToListAsync();  
        }

        public async Task<List<Material>> GetByClassIdAsync(string classId)
        {
            return await _context.Materials.Where(e => e.MaterialClassId.Equals(classId, StringComparison.OrdinalIgnoreCase))
                                           .Include(e => e.Properties)
                                           .ToListAsync();         
        }

        public async Task<Material?> GetByMaterialIdAsync(string materialId)
        {
            return await _context.Materials
                                 .Include(e => e.Properties)
                                 .FirstOrDefaultAsync(e => e.MaterialId.Equals(materialId, StringComparison.OrdinalIgnoreCase));
        }

        public void Update(Material material)
        {
            _context.Materials.Update(material);
        }
    }
}
