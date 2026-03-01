namespace WMS.Practice.Application.Queries.InventoryTrackingQueries.InventoryLogs
{
    public class GetAllInventoryLogsQueryHandler : IRequestHandler<GetAllInventoryLogsQuery, IEnumerable<InventoryLogDTO>>
    {
        private readonly IInventoryLogRepository _inventoryLogRepository;
        private readonly IMapper _mapper;

        public GetAllInventoryLogsQueryHandler(IInventoryLogRepository inventoryLogRepository, IMapper mapper)
        {
            _inventoryLogRepository = inventoryLogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryLogDTO>> Handle(GetAllInventoryLogsQuery request, CancellationToken cancellationToken)
        {
            var transactionType = request.TransactionType.ParseEnum<TransactionType>();
            var inventoryLogs = await _inventoryLogRepository.GetAllInventoryLogs(transactionType)
                             ?? throw new EntityNotFoundException($"Inventory Logs with Transaction Type {request.TransactionType} could not found");

            return _mapper.Map<IEnumerable<InventoryLogDTO>>(inventoryLogs);
        }
    }
}
