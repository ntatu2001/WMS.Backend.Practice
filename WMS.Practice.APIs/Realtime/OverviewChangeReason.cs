namespace WMS.Practice.APIs.Realtime
{
    /// <summary>
    /// Lý do màn hình Tổng quan cần được làm mới. Chỉ dùng làm tín hiệu gửi cho client,
    /// không phản ánh chi tiết số liệu.
    /// </summary>
    public enum OverviewChangeReason
    {
        ReceiptChanged,
        IssueChanged,
        StockTakeChanged,
        StockAdjustmentChanged,
        Other
    }
}
