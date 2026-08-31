using System.Collections.Concurrent;

namespace WMS.Practice.APIs.Realtime
{
    /// <summary>
    /// Singleton: giữ timer + hàng đợi reason xuyên request. Mỗi lần <see cref="QueueAsync"/>
    /// sẽ dời điểm bắn về sau <see cref="Window"/> kể từ thay đổi cuối, nên một burst nhiều
    /// thay đổi chỉ tạo ra một message <c>overviewChanged</c>.
    /// </summary>
    public sealed class OverviewChangeDebouncer : IOverviewChangeDebouncer, IAsyncDisposable
    {
        private static readonly TimeSpan Window = TimeSpan.FromSeconds(2);

        private readonly IHubContext<OverviewHub> _hub;
        private readonly ILogger<OverviewChangeDebouncer> _logger;
        private readonly ConcurrentDictionary<OverviewChangeReason, byte> _pending = new();
        private readonly SemaphoreSlim _flushGate = new(1, 1);
        private readonly object _timerLock = new();
        private Timer? _timer;

        public OverviewChangeDebouncer(IHubContext<OverviewHub> hub, ILogger<OverviewChangeDebouncer> logger)
        {
            _hub = hub;
            _logger = logger;
        }

        public Task QueueAsync(OverviewChangeReason reason, CancellationToken ct = default)
        {
            _pending[reason] = 1;

            lock (_timerLock)
            {
                _timer ??= new Timer(
                    static state => _ = ((OverviewChangeDebouncer)state!).FlushAsync(),
                    this, Timeout.Infinite, Timeout.Infinite);

                // Bắn một lần sau Window kể từ thay đổi cuối cùng.
                _timer.Change(Window, Timeout.InfiniteTimeSpan);
            }

            return Task.CompletedTask;
        }

        private async Task FlushAsync()
        {
            await _flushGate.WaitAsync();
            try
            {
                if (_pending.IsEmpty) return;

                var reasons = _pending.Keys.ToArray();
                _pending.Clear();

                var payload = new
                {
                    reason = reasons.Length == 1 ? reasons[0].ToString() : "Multiple",
                    reasons = reasons.Select(r => r.ToString()).ToArray(),
                    at = DateTime.UtcNow // ISO 8601 UTC khi serialize
                };

                await _hub.Clients.All.SendAsync("overviewChanged", payload);
                _logger.LogInformation("Pushed overviewChanged: {Reasons}", string.Join(",", payload.reasons));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to push overviewChanged");
            }
            finally
            {
                _flushGate.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_timer is not null) await _timer.DisposeAsync();
            _flushGate.Dispose();
        }
    }
}
