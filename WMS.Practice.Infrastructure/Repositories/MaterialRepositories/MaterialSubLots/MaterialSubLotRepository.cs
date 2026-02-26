namespace WMS.Practice.Infrastructure.Repositories.MaterialRepositories
{
    public class MaterialSubLotRepository : BaseRepository, IMaterialSubLotRepository
    {
        public MaterialSubLotRepository(WMSDbContext context) : base(context)
        {
        }

        public void Create(MaterialSubLot materialSubLot)
        {
            _context.MaterialSubLots.Add(materialSubLot);
        }

        public void Delete(MaterialSubLot materialSubLot)
        {
            _context.MaterialSubLots.Remove(materialSubLot);
        }

        public async Task<bool> ExistMaterialSubLotsByLotNumber(string lotNumber)
        {
            return await _context.MaterialSubLots.AnyAsync(x => x.LotNumber == lotNumber);
        }

        public async Task<bool> ExistsAsync(string materialSubLotId)
        {
            return await _context.MaterialSubLots.AnyAsync(x => x.MaterialSubLotId == materialSubLotId);
        }

        public async Task<List<MaterialSubLot>> GetAllMaterialSubLotsAsync()
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .ToListAsync();         
        }

        public async Task<MaterialSubLot?> GetMaterialSubLotByIdAsync(string Id)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .FirstOrDefaultAsync(e => e.MaterialSubLotId == Id);
        }

        public async Task<Material?> GetMaterialBySubLotIdAsync(string subLotId)
        {
            return await _context.MaterialSubLots.Where(x => x.MaterialSubLotId == subLotId)
                                                 .Select(x => x.MaterialLot.Material).FirstOrDefaultAsync();
        }

        public async Task<MaterialSubLot?> GetMaterialSubLotByLotNumberAndLocationId(string lotNumber, string locationId)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .FirstOrDefaultAsync(e => e.MaterialLot.LotNumber == lotNumber && e.LocationId == locationId);
        }

        public async Task<List<MaterialSubLot>> GetMaterialSubLotsByLocationIdAsync(string locationId)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .Where(e => e.LocationId == locationId)
                                 .ToListAsync();
        }

        public async Task<List<MaterialSubLot>> GetMaterialSubLotsByLotNumberAsync(string lotNumber)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .Where(e => e.LotNumber == lotNumber)
                                 .ToListAsync();
        }

        public async Task<List<MaterialSubLot>> GetMaterialSubLotsByStatusAsync(LotStatus status)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .Where(e => e.SubLotStatus == status)
                                 .ToListAsync();
        }

        public void Update(MaterialSubLot materialSubLot)
        {
            _context.MaterialSubLots.Update(materialSubLot);
        }
    }
}
