namespace WMS.Practice.Application.Queries.OverviewQueries
{
    public class GetInventoryActivityStatsQueryHandler : IRequestHandler<GetInventoryActivityStatsQuery, InventoryActivityStatsDTO>
    {
        private readonly IInventoryIssueRepository _inventoryIssueRepository;
        private readonly IInventoryReceiptRepository _inventoryReceiptRepository;
        private readonly IStockTakeRepository _stockTakeRepository;
        private readonly IOverviewService _overviewService;

        public GetInventoryActivityStatsQueryHandler(IInventoryIssueRepository inventoryIssueRepository, IInventoryReceiptRepository inventoryReceiptRepository, IStockTakeRepository stockTakeRepository, IOverviewService overviewService)
        {
            _inventoryIssueRepository = inventoryIssueRepository;
            _inventoryReceiptRepository = inventoryReceiptRepository;
            _stockTakeRepository = stockTakeRepository;
            _overviewService = overviewService;
        }

        public async Task<InventoryActivityStatsDTO> Handle(GetInventoryActivityStatsQuery request, CancellationToken cancellationToken)
        {
            var timeRange = _overviewService.GetTimeRange(request.TimeRange);

            var inventoryIssues = await _inventoryIssueRepository.GetInventoryIssuesByTimeRangeOption(timeRange.StartDate, timeRange.EndDate);
            var inventoryReceipts = await _inventoryReceiptRepository.GetInventoryReceiptsByTimeRangeOption(timeRange.StartDate, timeRange.EndDate);
            var stockTakes = await _stockTakeRepository.GetStockTakesByTimeRangeOption(timeRange.StartDate, timeRange.EndDate);

            // Issue Overview
            var totalIssues = inventoryIssues.Count;
            var completeIssues = inventoryIssues.Count(s => s.IssueStatus == IssueStatus.Done);
            var issueOverview = new IssueOverview(
                totalIssues: totalIssues,
                completeIssues: completeIssues,
                rateCompleteIssues: totalIssues == 0 ? 0 : Math.Round((completeIssues * 100.0) / totalIssues, 2)
            );

            // Receipt Overview
            var totalReceipts = inventoryReceipts.Count;
            var completeReceipts = inventoryReceipts.Count(s => s.ReceiptStatus == ReceiptStatus.Done);
            var receiptOverview = new ReceiptOverview(
                totalReceipts: totalReceipts,
                completeReceipts: completeReceipts,
                rateCompleteReceipts: totalReceipts == 0 ? 0 : Math.Round((completeReceipts * 100.0) / totalReceipts, 2)
            );

            // StockTake Overview
            var totalStockTakes = stockTakes.Count;
            var periodicStockTakes = stockTakes.Count(s => s.Type == AdjustmentType.Periodic);
            var stockTakeOverview = new StockTakeOverview(
                totalStockTakes: totalStockTakes,
                periodicStockTakes: periodicStockTakes,
                ratePeriodicStockTakes: totalStockTakes == 0 ? 0 : Math.Round((periodicStockTakes * 100.0) / totalStockTakes, 2)
            );

            // Total Overview
            var totalInventoryActivity = totalIssues + totalReceipts + totalStockTakes;
            var rateIssues = totalInventoryActivity == 0 ? 0 : Math.Round((totalIssues * 100.0) / totalInventoryActivity, 2);
            var rateReceipts = totalInventoryActivity == 0 ? 0 : Math.Round((totalReceipts * 100.0) / totalInventoryActivity, 2);
            var rateStockTakes = totalInventoryActivity == 0 ? 0 : 100.00 - rateIssues - rateReceipts;

            var totalOverview = new TotalOverview(
                totalIssues: totalIssues,
                totalReceipts: totalReceipts,
                totalStockTakes: totalStockTakes,
                rateIssues: rateIssues,
                rateReceipts: rateReceipts,
                rateStockTakes: rateStockTakes);

            return new InventoryActivityStatsDTO(issueOverview: issueOverview,
                                                 receiptOverview: receiptOverview,
                                                 stockTakeOverview: stockTakeOverview,
                                                 totalOverview: totalOverview);
        }
    }
}
