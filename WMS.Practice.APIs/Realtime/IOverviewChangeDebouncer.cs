namespace WMS.Practice.APIs.Realtime
{
    /// <summary>
    /// Gộp (debounce) nhiều thay đổi liên tiếp thành một lần phát <c>overviewChanged</c>
    /// mỗi ~2 giây, tránh spam client khi import/duyệt hàng loạt.
    /// </summary>
    public interface IOverviewChangeDebouncer
    {
        Task QueueAsync(OverviewChangeReason reason, CancellationToken ct = default);
    }
}
