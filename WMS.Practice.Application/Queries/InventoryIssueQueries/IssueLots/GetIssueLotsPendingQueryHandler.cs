namespace WMS.Practice.Application.Queries.InventoryIssueQueries.IssueLots
{
    public class GetIssueLotsPendingQueryHandler : IRequestHandler<GetIssueLotsPendingQuery, IEnumerable<IssueLotPendingDTO>>
    {
        private readonly IIssueLotRepository _issueLotRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public GetIssueLotsPendingQueryHandler(IIssueLotRepository issueLotRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _issueLotRepository = issueLotRepository;
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IssueLotPendingDTO>> Handle(GetIssueLotsPendingQuery request, CancellationToken cancellationToken)
        {
            var issueLots = await _issueLotRepository.GetIssueLotsPending()
                         ?? throw new EntityNotFoundException("Issue Lots could not found");

            issueLots = issueLots.Where(s => s.InventoryIssueEntry.InventoryIssue.WarehouseId == request.WarehouseId).ToList();

            var warehouseName = await _warehouseRepository.GetWarehouseNameByIdAsync(request.WarehouseId);

            var issueLotDTOs = _mapper.Map<IEnumerable<IssueLotPendingDTO>>(issueLots);
            foreach (var issueLotDTO in issueLotDTOs)
            {
                var issueLot = issueLots.First(x => x.IssueLotId == issueLotDTO.IssueLotId);

                issueLotDTO.MaterialId = issueLot.InventoryIssueEntry.MaterialId;
                issueLotDTO.MaterialName = issueLot.InventoryIssueEntry.MaterialName;
                issueLotDTO.LotNumber = issueLot.MaterialLotId;
                issueLotDTO.WarehouseName = warehouseName;
            }

            return issueLotDTOs;
        }
    }
}
