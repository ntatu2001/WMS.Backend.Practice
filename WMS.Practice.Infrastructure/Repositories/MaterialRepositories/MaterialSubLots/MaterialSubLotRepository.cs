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

        public async Task<List<MaterialSubLot>> GetAllAsync()
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .ToListAsync();         
        }

        public async Task<MaterialSubLot?> GetByIdAsync(string Id)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .FirstOrDefaultAsync(e => e.MaterialSubLotId == Id);
        }

        public async Task<List<MaterialSubLot>> GetMaterialSubLotsByLocationId(string locationId)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .Where(e => e.LocationId == locationId)
                                 .ToListAsync();
        }

        public async Task<List<MaterialSubLot>> GetMaterialSubLotsByLotNumber(string lotNumber)
        {
            return await _context.MaterialSubLots
                                 .Include(e => e.Location)
                                 .Include(e => e.MaterialLot)
                                 .Where(e => e.LotNumber == lotNumber)
                                 .ToListAsync();
        }

        public async Task<List<MaterialSubLot>> GetMaterialSubLotsByStatus(LotStatus status)
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
