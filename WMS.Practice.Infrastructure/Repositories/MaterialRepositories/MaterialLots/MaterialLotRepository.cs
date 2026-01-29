using System.Net.NetworkInformation;

namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class MaterialLotRepository : BaseRepository, IMaterialLotRepository
    {
        public MaterialLotRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(MaterialLot materialLot)
        {
            _context.MaterialLots.Add(materialLot);
        }

        public void Delete(MaterialLot materialLot)
        {
            _context.MaterialLots.Remove(materialLot);
        }

        public async Task<List<MaterialLot>> GetAllAsync()
        {
            return await _context.MaterialLots
                                 .Include(e => e.Properties)
                                 .Include(e => e.SubLots)
                                 .ToListAsync();
        }

        public async Task<MaterialLot?> GetMaterialLotByIdAsync(string lotNumber)
        {
            return await _context.MaterialLots
                                 .Include(e => e.Properties)
                                 .Include(e => e.SubLots)
                                 .FirstOrDefaultAsync(e => e.LotNumber == lotNumber);
        }

        public async Task<MaterialLot?> GetMaterialLotWithIssuesByIdAsync(string lotNumber)
        {
            return await _context.MaterialLots                                  
                                 .Include(e => e.IssueLots)
                                 .Include(e => e.SubLots)
                                 .FirstOrDefaultAsync(e => e.LotNumber == lotNumber);
        }

        public async Task<List<MaterialLot>> GetMaterialLotsByMaterialId(string materialId)
        {
            return await _context.MaterialLots
                                 .Where(e => e.MaterialId.Equals(materialId, StringComparison.OrdinalIgnoreCase))
                                 .Include(e => e.Properties)
                                 .Include(e => e.SubLots)
                                 .ToListAsync();
        }

        public async Task<List<MaterialLot>> GetMaterialLotsByStatus(LotStatus status)
        {
            return await _context.MaterialLots
                                 .Where(e => e.LotStatus == status)
                                 .Include(e => e.Properties)
                                 .Include(e => e.SubLots)
                                 .ToListAsync();
        }
    }
}
