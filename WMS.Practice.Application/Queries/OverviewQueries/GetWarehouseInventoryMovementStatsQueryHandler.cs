namespace WMS.Practice.Application.Queries.OverviewQueries
{
    public class GetWarehouseInventoryMovementStatsQueryHandler : IRequestHandler<GetWarehouseInventoryMovementStatsQuery, WarehouseInventoryMovementStatsDTO>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IOverviewService _overviewService;

        public GetWarehouseInventoryMovementStatsQueryHandler(IInventoryIssueRepository inventoryIssueRepository, IInventoryReceiptRepository inventoryReceiptRepository, IOverviewService overviewService)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _overviewService = overviewService;
        }

        public async Task<WarehouseInventoryMovementStatsDTO> Handle(GetWarehouseInventoryMovementStatsQuery request, CancellationToken cancellationToken)
        {
            var timeRange = _overviewService.GetTimeRange(request.TimeRange);

            var inventoryIssues = await _inventoryIssueRepository.GetInventoryIssuesByTimeRangeOption(timeRange.StartDate, timeRange.EndDate);
            var inventoryReceipts = await _inventoryReceiptRepository.GetInventoryReceiptsByTimeRangeOption(timeRange.StartDate, timeRange.EndDate);

            // WarehouseByReceiptDTO
            inventoryReceipts = inventoryReceipts
                .Where(x => x.ReceiptStatus != ReceiptStatus.Pending)
                .ToList();

            var warehouseByReceipt = new WarehouseByReceiptDTO
                                        (finishedProductQuantity: inventoryReceipts.Where(s => s.WarehouseId == "TP01").ToList().Count,
                                         semiFinishedProductQuantity: inventoryReceipts.Where(s => s.WarehouseId == "BTP01").ToList().Count,
                                         rawMaterialQuantity: inventoryReceipts.Where(s => s.WarehouseId == "NVL01").ToList().Count,
                                         materialQuantity: inventoryReceipts.Where(s => s.WarehouseId == "VT01").ToList().Count,
                                         packagingQuantity: inventoryReceipts.Where(s => s.WarehouseId == "BB01").ToList().Count);

            // WarehouseByIssueDTO
            inventoryIssues = inventoryIssues
                .Where(x => x.IssueStatus != IssueStatus.Pending)
                .ToList();

            var warehouseByIssue = new WarehouseByIssueDTO(
                                         finishedProductQuantity: inventoryIssues.Where(s => s.WarehouseId == "TP01").ToList().Count,
                                         semiFinishedProductQuantity: inventoryIssues.Where(s => s.WarehouseId == "BTP01").ToList().Count,
                                         rawMaterialQuantity: inventoryIssues.Where(s => s.WarehouseId == "NVL01").ToList().Count,
                                         materialQuantity: inventoryIssues.Where(s => s.WarehouseId == "VT01").ToList().Count,
                                         packagingQuantity: inventoryIssues.Where(s => s.WarehouseId == "BB01").ToList().Count);

            return new WarehouseInventoryMovementStatsDTO(warehouseByReceipt, warehouseByIssue);
        }
    }
}
