namespace WMS.Practice.APIs.Realtime
{
    /// <summary>
    /// Báo cho mọi client Dashboard biết số liệu tổng quan vừa thay đổi.
    /// v1 phát tín hiệu qua EF SaveChanges interceptor; interface này giữ sẵn để
    /// có thể gọi tường minh trong Command Handler khi cần reason chính xác hơn.
    /// </summary>
    public interface IOverviewNotifier
    {
        Task NotifyOverviewChangedAsync(OverviewChangeReason reason, CancellationToken ct = default);
    }

    public sealed class OverviewNotifier : IOverviewNotifier
    {
        private readonly IOverviewChangeDebouncer _debouncer;

        public OverviewNotifier(IOverviewChangeDebouncer debouncer) => _debouncer = debouncer;

        public Task NotifyOverviewChangedAsync(OverviewChangeReason reason, CancellationToken ct = default)
            => _debouncer.QueueAsync(reason, ct);
    }
}
