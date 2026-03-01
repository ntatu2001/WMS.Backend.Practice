namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryLogs
{
    public class GetInventoryLogByLotNumberQueryHandler : IRequestHandler<GetInventoryLogByLotNumberQuery, IEnumerable<InventoryLogDTO>>
    {
        private readonly IInventoryLogRepository _inventoryLogRepository;
        private readonly IMapper _mapper;

        public GetInventoryLogByLotNumberQueryHandler(IInventoryLogRepository inventoryLogRepository, IMapper mapper)
        {
            _inventoryLogRepository = inventoryLogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryLogDTO>> Handle(GetInventoryLogByLotNumberQuery request, CancellationToken cancellationToken)
        {
            var lotStatus = request.Status.ParseEnum<LotStatus>();
            var inventoryLogs = await _inventoryLogRepository.GetInventoryLogByLotNumberAndStatus(request.LotNumber, lotStatus)
                             ?? throw new EntityNotFoundException($"Inventory Log with Lot Number {request.LotNumber} and Lot Status {request.Status} could not found");

            return _mapper.Map<IEnumerable<InventoryLogDTO>>(inventoryLogs);
        }
    }
}
