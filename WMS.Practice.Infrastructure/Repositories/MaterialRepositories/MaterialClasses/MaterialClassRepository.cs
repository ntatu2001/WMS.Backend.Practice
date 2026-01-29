namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class MaterialClassRepository : BaseRepository, IMaterialClassRepository
    {
        public MaterialClassRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(MaterialClass materialClass)
        {
            _context.MaterialClasses.Add(materialClass);
        }

        public void Delete(MaterialClass materialClass)
        {
            _context.MaterialClasses.Remove(materialClass);
        }

        public async Task<List<MaterialClass>> GetAllAsync()
        {
            return await _context.MaterialClasses
                                 .Include(mc => mc.Properties)
                                 .Include(e => e.Materials)
                                    .ThenInclude(e => e.MaterialName)
                                 .ToListAsync();
        }

        public async Task<MaterialClass?> GetByClassIdAsync(string classId)
        {
            return await _context.MaterialClasses
                                 .Include(mc => mc.Properties)    
                                 .Include(e => e.Materials)
                                    .ThenInclude(e => e.Properties)
                                 .FirstOrDefaultAsync(e => e.MaterialClassId.Equals(classId, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<List<MaterialClass>> GetByClassIdsAsync(List<string> classIds)
        {
            return await _context.MaterialClasses
                                 .Where(e => classIds.Contains(e.MaterialClassId))
                                 .Include(mc => mc.Properties)
                                 .Include(e => e.Materials)
                                    .ThenInclude(e => e.Properties)
                                 .ToListAsync();
        }

        public void Update(MaterialClass materialClass)
        {
            _context.MaterialClasses.Update(materialClass);
        }
    }
}
