namespace WMS.Practice.Application.DTOs.OverviewDTOs
{
    public class InventoryActivityStatsDTO
    {
        public IssueOverview IssueOverview { get; set; }
        public ReceiptOverview ReceiptOverview { get; set; }
        public StockTakeOverview StockTakeOverview { get; set; }
        public TotalOverview TotalOverview { get; set; }

        public InventoryActivityStatsDTO(IssueOverview issueOverview, ReceiptOverview receiptOverview, StockTakeOverview stockTakeOverview, TotalOverview totalOverview)
        {
            IssueOverview = issueOverview;
            ReceiptOverview = receiptOverview;
            StockTakeOverview = stockTakeOverview;
            TotalOverview = totalOverview;
        }
    }

    public class IssueOverview
    {
        public int TotalIssues { get; set; }
        public int CompleteIssues { get; set; }
        public double RateCompleteIssues { get; set; }

        public IssueOverview(int totalIssues, int completeIssues, double rateCompleteIssues)
        {
            TotalIssues = totalIssues;
            CompleteIssues = completeIssues;
            RateCompleteIssues = rateCompleteIssues;
        }
    }
    public class ReceiptOverview
    {
        public int TotalReceipts { get; set; }
        public int CompleteReceipts { get; set; }
        public double RateCompleteReceipts { get; set; }

        public ReceiptOverview(int totalReceipts, int completeReceipts, double rateCompleteReceipts)
        {
            TotalReceipts = totalReceipts;
            CompleteReceipts = completeReceipts;
            RateCompleteReceipts = rateCompleteReceipts;
        }
    }
    public class StockTakeOverview
    {
        public int TotalStockTakes { get; set; }
        public int PeriodicStockTakes { get; set; }
        public double RatePeriodicStockTakes { get; set; }

        public StockTakeOverview(int totalStockTakes, int periodicStockTakes, double ratePeriodicStockTakes)
        {
            TotalStockTakes = totalStockTakes;
            PeriodicStockTakes = periodicStockTakes;
            RatePeriodicStockTakes = ratePeriodicStockTakes;
        }
    }

    public class TotalOverview
    {
        public int TotalIssues { get; set; }
        public int TotalReceipts { get; set; }
        public int TotalStockTakes { get; set; }
        public double RateIssues { get; set; }
        public double RateReceipts { get; set; }
        public double RateStockTakes { get; set; }

        public TotalOverview(int totalIssues, int totalReceipts, int totalStockTakes, double rateIssues, double rateReceipts, double rateStockTakes)
        {
            TotalIssues = totalIssues;
            TotalReceipts = totalReceipts;
            TotalStockTakes = totalStockTakes;
            RateIssues = rateIssues;
            RateReceipts = rateReceipts;
            RateStockTakes = rateStockTakes;
        }
    }


}
