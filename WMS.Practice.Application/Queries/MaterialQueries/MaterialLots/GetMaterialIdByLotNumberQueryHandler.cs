namespace WMS.Practice.Application.Queries.MaterialQueries.MaterialLots
{
    public class GetMaterialIdByLotNumberQueryHandler : IRequestHandler<GetMaterialIdByLotNumberQuery, string>
    {
        private readonly IMaterialLotRepository _materialLotRepository;

        public GetMaterialIdByLotNumberQueryHandler(IMaterialLotRepository materialLotRepository)
        {
            _materialLotRepository = materialLotRepository;
        }

        public async Task<string> Handle(GetMaterialIdByLotNumberQuery request, CancellationToken cancellationToken)
        {
            return await _materialLotRepository.GetMaterialIdByLotNumberAsync(request.LotNumber)
                ?? throw new EntityNotFoundException($"Material Lot with Id {request.LotNumber} could not found");
        }
    }
}
