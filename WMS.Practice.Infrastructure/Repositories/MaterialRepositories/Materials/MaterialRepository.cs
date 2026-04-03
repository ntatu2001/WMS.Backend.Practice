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

        public async Task<List<Material>> GetAllMaterialsAsync()
        {
            return await _context.Materials
                                 .Include(e => e.Properties)
                                 .ToListAsync();  
        }

        public async Task<List<Material>> GetMaterialsByClassIdAsync(string classId)
        {
            return await _context.Materials.Where(e => e.MaterialClassId == classId)
                                           .Include(e => e.Properties)
                                           .ToListAsync();         
        }

        public async Task<Material?> GetMaterialByIdAsync(string materialId)
        {
            return await _context.Materials
                                 .Include(e => e.Properties)
                                 .FirstOrDefaultAsync(e => e.MaterialId == materialId);
        }

        public void Update(Material material)
        {
            _context.Materials.Update(material);
        }

        public async Task<List<Material>> GetMaterialsByClassIdAndMaterialLots(string classId)
        {
            return await _context.Materials.Include(e => e.MaterialLots)
                                           .Where(e => e.MaterialClassId == classId
                                                    && e.MaterialLots.Count > 0)
                                           .ToListAsync();
        }

        public async Task<string?> GetMaterialNameByIdAsync(string materialId)
        {
            return await _context.Materials.Where(e => e.MaterialId == materialId)
                                           .Select(e => e.MaterialName)
                                           .FirstOrDefaultAsync();
        }
    }
}
