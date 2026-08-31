using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WMS.Practice.APIs.Realtime
{
    /// <summary>
    /// Singleton EF interceptor: lưới an toàn phát tín hiệu <c>overviewChanged</c> sau mọi
    /// <c>SaveChanges</c> có chạm tới entity ảnh hưởng số liệu màn hình Tổng quan.
    ///
    /// Hai pha vì sau <c>base.SaveChangesAsync</c> EF đã chạy AcceptAllChangesOnSave
    /// (mọi entry về Unchanged):
    ///   - <c>SavingChanges*</c>: quét ChangeTracker, suy ra tập reason, stash theo instance DbContext.
    ///   - <c>SavedChanges*</c>: đẩy reason đã stash vào debouncer (sau khi lưu thành công).
    ///   - <c>SaveChangesFailed*</c>: xoá stash để context tái sử dụng không phát reason cũ.
    /// </summary>
    public sealed class OverviewChangeInterceptor : SaveChangesInterceptor
    {
        // Tên class entity (GetType().Name) -> reason. Đối chiếu DbSet trong WMSDbContext.
        private static readonly IReadOnlyDictionary<string, OverviewChangeReason> Watched =
            new Dictionary<string, OverviewChangeReason>(StringComparer.Ordinal)
            {
                ["InventoryReceipt"] = OverviewChangeReason.ReceiptChanged,
                ["InventoryReceiptEntry"] = OverviewChangeReason.ReceiptChanged,
                ["ReceiptLot"] = OverviewChangeReason.ReceiptChanged,
                ["ReceiptSubLot"] = OverviewChangeReason.ReceiptChanged,
                ["InventoryIssue"] = OverviewChangeReason.IssueChanged,
                ["InventoryIssueEntry"] = OverviewChangeReason.IssueChanged,
                ["IssueLot"] = OverviewChangeReason.IssueChanged,
                ["IssueSubLot"] = OverviewChangeReason.IssueChanged,
                ["StockTake"] = OverviewChangeReason.StockTakeChanged,
                ["StockTakeSubLot"] = OverviewChangeReason.StockTakeChanged,
                ["MaterialLot"] = OverviewChangeReason.StockAdjustmentChanged,
                ["MaterialSubLot"] = OverviewChangeReason.StockAdjustmentChanged,
            };

        // State giữa 2 pha, key theo instance DbContext. EF cấm thao tác đồng thời trên
        // một context nên không cần khoá; bảng tự thu hồi khi context bị GC.
        private static readonly ConditionalWeakTable<DbContext, OverviewChangeReason[]> _pending = new();

        private readonly IOverviewChangeDebouncer _debouncer;

        public OverviewChangeInterceptor(IOverviewChangeDebouncer debouncer) => _debouncer = debouncer;

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData, InterceptionResult<int> result)
        {
            Capture(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct = default)
        {
            Capture(eventData.Context);
            return base.SavingChangesAsync(eventData, result, ct);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            FlushAsync(eventData.Context, CancellationToken.None).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, int result, CancellationToken ct = default)
        {
            await FlushAsync(eventData.Context, ct);
            return await base.SavedChangesAsync(eventData, result, ct);
        }

        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            if (eventData.Context is not null) _pending.Remove(eventData.Context);
            base.SaveChangesFailed(eventData);
        }

        public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken ct = default)
        {
            if (eventData.Context is not null) _pending.Remove(eventData.Context);
            return base.SaveChangesFailedAsync(eventData, ct);
        }

        private static void Capture(DbContext? context)
        {
            if (context is null) return;

            var reasons = new HashSet<OverviewChangeReason>();
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted))
                    continue;

                if (Watched.TryGetValue(entry.Entity.GetType().Name, out var reason))
                    reasons.Add(reason);
            }

            _pending.Remove(context);
            if (reasons.Count > 0)
                _pending.Add(context, reasons.ToArray());
        }

        private async Task FlushAsync(DbContext? context, CancellationToken ct)
        {
            if (context is null) return;
            if (!_pending.TryGetValue(context, out var reasons)) return;

            _pending.Remove(context);

            foreach (var reason in reasons)
                await _debouncer.QueueAsync(reason, ct);
        }
    }
}
