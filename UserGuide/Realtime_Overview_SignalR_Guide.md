# Realtime Update cho màn hình Tổng quan (Dashboard) — Hướng dẫn Backend triển khai SignalR

> Tài liệu này hướng dẫn đội Backend (`WMS.Practice.APIs` / ASP.NET Core) bổ sung cơ chế **realtime push** bằng **SignalR** cho 2 API đang được dùng ở màn hình **Tổng quan** của Frontend (ReactJS).
>
> Đọc kèm:
> - [`Authentication_Authorization_Guide.md`](./Authentication_Authorization_Guide.md) — cơ chế JWT Bearer + Refresh Token đang áp dụng cho toàn bộ API. SignalR sẽ tái sử dụng đúng access token này.
> - [`API_Guide_For_Frontend.md`](./API_Guide_For_Frontend.md) — chi tiết các API nghiệp vụ (Receipt / Issue / StockTake...) là nguồn phát sinh thay đổi dữ liệu tổng quan.
>
> Cập nhật lần cuối: 2026-08-30, dựa trên nhánh `main`.

---

## 1. Mục tiêu & phạm vi

Màn hình Tổng quan ([`src/features/Dashboard/presentation/Dashboard.jsx`](../src/features/Dashboard/presentation/Dashboard.jsx)) hiện gọi **2 endpoint REST** (xem [`src/api/overView.js`](../src/api/overView.js)):

| # | Endpoint | Query param | Trả về (rút gọn) |
|---|---|---|---|
| 1 | `GET /WarehouseAPI/Overview/GetInventoryActivityStats` | `timeRangeOption = Today \| ThisWeek \| ThisMonth` | `receiptOverview`, `issueOverview`, `stockTakeOverview`, `totalOverview` (tổng / hoàn thành / định kỳ...) |
| 2 | `GET /WarehouseAPI/Overview/GetWarehouseInventoryMovementStats` | `timeRangeOption = Today \| ThisWeek \| ThisMonth` | `warehouseByReceipt`, `warehouseByIssue` (sản lượng theo 5 loại kho) |

**Mục tiêu**: khi có nghiệp vụ làm thay đổi số liệu tổng quan (tạo/duyệt/hoàn thành/xoá phiếu **Nhập kho**, **Xuất kho**, **Kiểm kê**), Backend **chủ động đẩy 1 tín hiệu** qua SignalR để Frontend tự gọi lại 2 endpoint trên và vẽ lại biểu đồ — không cần người dùng F5.

### Nguyên tắc thiết kế (quan trọng)

- **Đẩy tín hiệu nhẹ ("có thay đổi"), KHÔNG đẩy toàn bộ số liệu.** Server chỉ gửi 1 message nhỏ dạng `{ reason, at }`. Frontend nhận được thì gọi lại 2 REST API (đã có sẵn, đã có auth, đã có cache-busting phía FE). Lý do:
  - Không phải tính lại toàn bộ thống kê cho từng connection.
  - Không phải giữ đồng bộ 2 "đường" trả dữ liệu (REST + WebSocket) — REST vẫn là nguồn sự thật duy nhất.
  - Payload nhỏ ⇒ dễ debounce, dễ test, ít rủi ro lộ dữ liệu.
- **Broadcast cho tất cả client đã đăng nhập.** Số liệu tổng quan không phân quyền theo role (xem Auth Guide mục 4), nên chỉ cần `[Authorize]` ở Hub là đủ, gửi tới `Clients.All`. Chưa cần Groups.
- **Debounce ở server** để gộp nhiều thay đổi liên tiếp (import Excel nhiều dòng, duyệt hàng loạt...) thành 1 lần đẩy.

---

## 2. Kiến trúc tổng thể

```
                                   ┌─────────────────────────────────────────┐
   Nghiệp vụ thay đổi dữ liệu       │              ASP.NET Core API            │
   (CreateReceipt, CompleteIssue,  │                                         │
    CreateStockTake, Delete...)    │  Command Handler / EF SaveChanges        │
              │                    │        interceptor                      │
              │  gọi                │             │                           │
              ▼                    │             ▼                           │
      IOverviewNotifier ───────────┼──►  OverviewChangeDebouncer (throttle)  │
                                   │             │                           │
                                   │             ▼                           │
                                   │   IHubContext<OverviewHub>              │
                                   │   .Clients.All.SendAsync(               │
                                   │       "overviewChanged", payload)       │
                                   └─────────────┬───────────────────────────┘
                                                 │  WebSocket (JWT qua query string)
                                                 ▼
                                   ┌─────────────────────────────────────────┐
                                   │   Frontend  @microsoft/signalr          │
                                   │   .on("overviewChanged", () => {        │
                                   │       refetch GetInventoryActivityStats │
                                   │       refetch GetWarehouseInventoryMove…│
                                   │   })                                    │
                                   └─────────────────────────────────────────┘
```

---

## 3. Các bước triển khai Backend

### 3.1. Cài đặt / bật SignalR

SignalR nằm sẵn trong ASP.NET Core (`Microsoft.AspNetCore.SignalR`), **không cần NuGet package** cho bản self-hosted 1 instance.

```csharp
// Program.cs
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
    options.KeepAliveInterval    = TimeSpan.FromSeconds(15);
});
```

### 3.2. Tạo Hub

```csharp
// Realtime/OverviewHub.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WMS.Practice.APIs.Realtime;

[Authorize] // bắt buộc đăng nhập, dùng đúng JWT Bearer như REST
public sealed class OverviewHub : Hub
{
    // Không cần method nào để client gọi lên (one-way: server -> client).
    // Giữ class rỗng; có thể log connect/disconnect nếu cần.
    public override async Task OnConnectedAsync()
    {
        // (tuỳ chọn) _logger.LogInformation("Overview client connected: {ConnId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }
}
```

### 3.3. Map endpoint Hub

```csharp
// Program.cs — sau app.MapControllers()
app.MapHub<OverviewHub>("/WarehouseAPI/hubs/overview");
```

> **Chốt với Frontend**: URL hub đầy đủ là `http://localhost:5037/WarehouseAPI/hubs/overview`. Nếu đổi path, phải báo FE cập nhật.

### 3.4. Cho phép JWT đi qua WebSocket (query string)

Trình duyệt **không gắn được header `Authorization`** cho kết nối WebSocket. SignalR chuẩn hoá bằng cách truyền token qua query string `?access_token=...`. Cần bổ sung `OnMessageReceived` vào cấu hình JWT Bearer hiện có:

```csharp
// Chỗ đang cấu hình .AddJwtBearer(...) trong Program.cs
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = /* ...giữ nguyên cấu hình hiện tại... */;

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) &&
                path.StartsWithSegments("/WarehouseAPI/hubs"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
```

- Chỉ áp dụng cho path bắt đầu bằng `/WarehouseAPI/hubs` để không ảnh hưởng REST.
- Token trong query string sẽ xuất hiện trong log web server nếu bật request logging — nên **tắt logging query string cho path hub**, hoặc chấp nhận rủi ro ở môi trường học tập (access token sống 30 phút).
- SignalR chỉ gửi token qua query string ở **HTTP negotiate / WebSocket handshake đầu tiên**; các message sau đi trong kết nối đã mở.

### 3.5. Cấu hình CORS cho SignalR

⚠️ Auth Guide mục 7 nói Backend đang bật policy `AllowAll` (mọi origin). **SignalR (khi client bật `withCredentials`, mặc định của thư viện JS) KHÔNG chấp nhận `AllowAnyOrigin`.** Cần 1 policy có origin tường minh + `AllowCredentials`:

```csharp
const string CorsPolicy = "WmsCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
    {
        policy.WithOrigins(
                  "http://localhost:5173",   // Vite dev (đối chiếu vite.config)
                  "http://localhost:4173"    // vite preview
                  // + domain production khi deploy
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();           // bắt buộc cho SignalR
    });
});

// ...
app.UseCors(CorsPolicy);   // PHẢI đứng trước UseAuthentication / MapHub
```

> Nếu muốn giữ `AllowAll` cho REST và chỉ siết cho hub: tạo 2 policy, gắn policy có `AllowCredentials` riêng cho `MapHub<OverviewHub>(...).RequireCors(CorsPolicy)`.

### 3.6. Service phát tín hiệu — `IOverviewNotifier`

Bọc `IHubContext` sau 1 interface để tầng nghiệp vụ không phụ thuộc trực tiếp SignalR và dễ mock khi test.

```csharp
// Realtime/IOverviewNotifier.cs
public interface IOverviewNotifier
{
    /// <summary>Báo cho mọi client Dashboard biết số liệu tổng quan vừa thay đổi.</summary>
    Task NotifyOverviewChangedAsync(OverviewChangeReason reason, CancellationToken ct = default);
}

public enum OverviewChangeReason
{
    ReceiptChanged,
    IssueChanged,
    StockTakeChanged,
    StockAdjustmentChanged,
    Other
}
```

```csharp
// Realtime/OverviewNotifier.cs
using Microsoft.AspNetCore.SignalR;

public sealed class OverviewNotifier : IOverviewNotifier
{
    private readonly IHubContext<OverviewHub> _hub;
    private readonly IOverviewChangeDebouncer _debouncer;

    public OverviewNotifier(IHubContext<OverviewHub> hub, IOverviewChangeDebouncer debouncer)
    {
        _hub = hub;
        _debouncer = debouncer;
    }

    public Task NotifyOverviewChangedAsync(OverviewChangeReason reason, CancellationToken ct = default)
        // Không await trực tiếp SendAsync ở đây — đẩy vào debouncer để gộp burst.
        => _debouncer.QueueAsync(reason, ct);
}
```

```csharp
// Program.cs
builder.Services.AddSingleton<IOverviewChangeDebouncer, OverviewChangeDebouncer>();
builder.Services.AddScoped<IOverviewNotifier, OverviewNotifier>();
```

### 3.7. Debounce / throttle (gộp burst)

Import Excel nhiều dòng hoặc duyệt hàng loạt có thể tạo hàng chục thay đổi trong 1 giây. Gộp lại thành **1 lần đẩy mỗi ~2 giây**:

```csharp
// Realtime/OverviewChangeDebouncer.cs
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

public interface IOverviewChangeDebouncer
{
    Task QueueAsync(OverviewChangeReason reason, CancellationToken ct = default);
}

public sealed class OverviewChangeDebouncer : IOverviewChangeDebouncer, IAsyncDisposable
{
    private static readonly TimeSpan Window = TimeSpan.FromSeconds(2);

    private readonly IHubContext<OverviewHub> _hub;
    private readonly ILogger<OverviewChangeDebouncer> _logger;
    private readonly ConcurrentDictionary<OverviewChangeReason, byte> _pending = new();
    private readonly SemaphoreSlim _gate = new(1, 1);
    private Timer? _timer;

    public OverviewChangeDebouncer(IHubContext<OverviewHub> hub, ILogger<OverviewChangeDebouncer> logger)
    {
        _hub = hub;
        _logger = logger;
    }

    public Task QueueAsync(OverviewChangeReason reason, CancellationToken ct = default)
    {
        _pending[reason] = 1;
        // (re)arm timer: chỉ bắn 1 lần sau Window kể từ thay đổi cuối
        _timer ??= new Timer(async _ => await FlushAsync(), null, Timeout.Infinite, Timeout.Infinite);
        _timer.Change(Window, Timeout.InfiniteTimeSpan);
        return Task.CompletedTask;
    }

    private async Task FlushAsync()
    {
        await _gate.WaitAsync();
        try
        {
            if (_pending.IsEmpty) return;
            var reasons = _pending.Keys.ToArray();
            _pending.Clear();

            var payload = new
            {
                reason = reasons.Length == 1 ? reasons[0].ToString() : "Multiple",
                reasons = reasons.Select(r => r.ToString()).ToArray(),
                at = DateTime.UtcNow            // ISO 8601 UTC
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
            _gate.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_timer is not null) await _timer.DisposeAsync();
        _gate.Dispose();
    }
}
```

> Debouncer là `Singleton` (giữ timer & hàng đợi xuyên request). Với 1 instance API là đủ; khi scale-out nhiều instance xem mục 6.

### 3.8. Gọi `NotifyOverviewChangedAsync` ở đâu

Cần gọi sau **mọi thao tác ghi thành công** làm đổi số liệu của 2 endpoint tổng quan. Có 2 cách, chọn 1:

#### Cách A — Gọi tường minh trong Command Handler (rõ ràng, kiểm soát tốt)

Inject `IOverviewNotifier` vào các handler sau và gọi **sau khi `SaveChanges` thành công**:

| Nhóm nghiệp vụ | Handler / thao tác cần gắn | Reason |
|---|---|---|
| **Nhập kho** | Tạo phiếu nhập, Cập nhật, Duyệt/Hoàn thành (chuyển khỏi trạng thái Pending), Xoá; tạo/sửa Receipt Entry, import Excel | `ReceiptChanged` |
| **Xuất kho** | Tạo phiếu xuất, Cập nhật, Duyệt/Hoàn thành, Xoá; tạo/sửa Issue Entry, import Excel | `IssueChanged` |
| **Kiểm kê** (StockTake — trước đây `MaterialLotAdjustment`) | Tạo phiếu kiểm kê, Cập nhật, Hoàn thành, Xoá | `StockTakeChanged` |
| Điều chỉnh tồn kho / lot ảnh hưởng sản lượng theo kho | Các lệnh ghi lên `...Lot` / tồn kho | `StockAdjustmentChanged` |

```csharp
// Ví dụ trong handler tạo phiếu nhập
public async Task<Guid> Handle(CreateReceiptCommand cmd, CancellationToken ct)
{
    // ...tạo entity, _db.Add(...), _db.SaveChangesAsync(ct)...
    await _overviewNotifier.NotifyOverviewChangedAsync(OverviewChangeReason.ReceiptChanged, ct);
    return receipt.Id;
}
```

#### Cách B — EF Core `SaveChanges` interceptor (bắt tất cả, ít sót)

Bắt theo loại entity bị thay đổi, không cần sửa từng handler:

```csharp
// Realtime/OverviewChangeInterceptor.cs
using Microsoft.EntityFrameworkCore.Diagnostics;

public sealed class OverviewChangeInterceptor : SaveChangesInterceptor
{
    private static readonly HashSet<string> Watched = new(StringComparer.Ordinal)
    {
        // Điền đúng tên class entity trong domain:
        "Receipt", "ReceiptEntry", "ReceiptLot",
        "Issue",   "IssueEntry",   "IssueLot",
        "StockTake", "StockTakeEntry",
        "MaterialLot", "InventoryStock"
    };

    private readonly IServiceScopeFactory _scopeFactory;
    public OverviewChangeInterceptor(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData, int result, CancellationToken ct = default)
    {
        var ctx = eventData.Context;
        if (ctx is not null)
        {
            var touched = ctx.ChangeTracker.Entries()
                .Any(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted
                          && Watched.Contains(e.Entity.GetType().Name));

            if (touched)
            {
                using var scope = _scopeFactory.CreateScope();
                var notifier = scope.ServiceProvider.GetRequiredService<IOverviewNotifier>();
                await notifier.NotifyOverviewChangedAsync(OverviewChangeReason.Other, ct);
            }
        }
        return await base.SavedChangesAsync(eventData, result, ct);
    }
}
```

```csharp
// Đăng ký interceptor
builder.Services.AddScoped<OverviewChangeInterceptor>();

builder.Services.AddDbContext<AppDbContext>((sp, opt) =>
{
    opt.UseSqlServer(connStr);
    opt.AddInterceptors(sp.GetRequiredService<OverviewChangeInterceptor>());
});
```

> **Khuyến nghị**: dùng **Cách B** làm lưới an toàn (không sót), có thể bổ sung **Cách A** ở vài handler quan trọng nếu muốn `reason` chính xác hơn. Nếu domain đã có **Domain Events / MediatR notifications**, phát 1 notification `OverviewChangedNotification` và xử lý ở 1 handler gọi `IOverviewNotifier` cũng tương đương Cách A.

---

## 4. Hợp đồng message (chốt với Frontend)

| Thuộc tính | Giá trị |
|---|---|
| Hub URL | `http://localhost:5037/WarehouseAPI/hubs/overview` |
| Transport | WebSockets (fallback SSE / Long Polling do thư viện tự lo) |
| Auth | JWT Bearer access token qua `?access_token=` (mục 3.4) — cùng token với REST |
| Tên event server → client | `overviewChanged` |
| Chiều client → server | **Không có** (one-way) |
| Tần suất tối đa | 1 message / ~2 giây (debounce, mục 3.7) |

**Payload** (`overviewChanged`):

```ts
{
  reason: "ReceiptChanged" | "IssueChanged" | "StockTakeChanged"
        | "StockAdjustmentChanged" | "Other" | "Multiple";
  reasons: string[];      // danh sách reason đã gộp trong cửa sổ debounce
  at: string;             // ISO 8601 DateTime (UTC), thời điểm server đẩy
}
```

**FE sẽ xử lý**: nhận `overviewChanged` ⇒ gọi lại **cả 2** endpoint mục 1 với `timeRangeOption` đang hiển thị (và làm mới cache nội bộ). FE **không** đọc số liệu từ payload — payload chỉ là tín hiệu.

> Nếu sau này Backend muốn cho FE biết **khoảng thời gian nào bị ảnh hưởng** (chỉ `Today` hay cả `ThisWeek`/`ThisMonth`) để FE refetch chọn lọc, thêm field `affectedTimeRanges: ("Today"|"ThisWeek"|"ThisMonth")[]` vào payload và báo FE. Chưa cần cho bản đầu.

---

## 5. Kiểm thử

Swagger **không** test được Hub. Dùng 1 trong các cách:

### 5.1. Script Node.js nhanh

```bash
npm i @microsoft/signalr
```

```js
// test-overview-hub.mjs
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const ACCESS_TOKEN = process.env.TOKEN; // lấy từ POST /WarehouseAPI/Auth/Login

const conn = new HubConnectionBuilder()
  .withUrl("http://localhost:5037/WarehouseAPI/hubs/overview", {
    accessTokenFactory: () => ACCESS_TOKEN,
  })
  .configureLogging(LogLevel.Information)
  .withAutomaticReconnect()
  .build();

conn.on("overviewChanged", (payload) => {
  console.log("⚡ overviewChanged:", payload);
});

await conn.start();
console.log("Connected. Đang chờ event... (hãy tạo/sửa/xoá 1 phiếu nhập-xuất-kiểm kê)");
```

```bash
TOKEN="eyJhbGciOi..." node test-overview-hub.mjs
```

### 5.2. Checklist test thủ công

- [ ] Kết nối không kèm token ⇒ bị `401` ở bước negotiate (không connect được).
- [ ] Kết nối với token hợp lệ ⇒ `Connected`.
- [ ] Tạo 1 phiếu nhập kho ⇒ trong ≤ ~2s nhận được `overviewChanged` với `reason` chứa `ReceiptChanged` (hoặc `Other` nếu dùng Cách B).
- [ ] Import Excel 20 dòng ⇒ chỉ nhận **1** message (debounce hoạt động), không phải 20.
- [ ] Hoàn thành / xoá phiếu xuất, tạo phiếu kiểm kê ⇒ đều nhận message.
- [ ] Token hết hạn khi kết nối đang mở ⇒ server đóng connection; client (thư viện) sẽ `reconnect` và `accessTokenFactory` cấp token mới. Xác nhận reconnect thành công.
- [ ] 2 tab trình duyệt cùng đăng nhập ⇒ cả 2 đều nhận message (broadcast `Clients.All`).

---

## 6. Lưu ý khi deploy / scale-out

| Vấn đề | Xử lý |
|---|---|
| **Nhiều instance API** (load balancer) | SignalR in-memory chỉ broadcast trong 1 instance. Cần **backplane**: `Microsoft.AspNetCore.SignalR.StackExchangeRedis` (`AddSignalR().AddStackExchangeRedis(redisConn)`) hoặc **Azure SignalR Service**. Debouncer khi đó nên chuyển sang cơ chế phân tán (hoặc chấp nhận mỗi instance debounce riêng). |
| **Sticky session** | Nếu không dùng WebSocket thuần (fallback về Long Polling) và có >1 instance ⇒ cần bật sticky session ở LB. Dùng WebSocket + Redis backplane thì không cần. |
| **Reverse proxy (IIS / Nginx)** | Bật `WebSocket` module (IIS) / `proxy_set_header Upgrade` + `Connection "upgrade"` (Nginx). Tăng `proxy_read_timeout` ≥ 100s. |
| **Token trong query string bị ghi log** | Loại path `/WarehouseAPI/hubs` khỏi request logging, hoặc mask query string. |
| **Số lượng connection** | Mỗi client Dashboard giữ 1 connection khi ở trên trang. Đóng khi rời trang (FE tự `stop()`). Với quy mô nội bộ (vài chục user) không cần lo giới hạn. |

---

## 7. Checklist cho Backend Developer

- [ ] `builder.Services.AddSignalR(...)` trong `Program.cs`
- [ ] Tạo `OverviewHub : Hub` gắn `[Authorize]`
- [ ] `app.MapHub<OverviewHub>("/WarehouseAPI/hubs/overview")`
- [ ] Thêm `JwtBearerEvents.OnMessageReceived` đọc `access_token` từ query string cho path `/WarehouseAPI/hubs`
- [ ] Thêm CORS policy có origin tường minh + `AllowCredentials()`, đặt `UseCors` trước `UseAuthentication` và `MapHub`
- [ ] Tạo `IOverviewNotifier` + `OverviewNotifier` (Scoped) và `OverviewChangeDebouncer` (Singleton)
- [ ] Gắn phát tín hiệu: EF `SaveChanges` interceptor (Cách B) và/hoặc gọi tường minh trong handler Receipt / Issue / StockTake (Cách A)
- [ ] Đặt tên event đúng `overviewChanged`, payload `{ reason, reasons, at }`
- [ ] Test bằng script Node mục 5.1 + checklist mục 5.2
- [ ] Xác nhận lại với Frontend: hub URL, tên event, cấu trúc payload — trước khi FE tích hợp
- [ ] (Khi lên nhiều instance) thêm Redis backplane hoặc Azure SignalR Service

---

## 8. Việc phía Frontend (tham khảo — không thuộc phạm vi Backend)

Để Backend hình dung đầu bên kia, FE sẽ:

1. `npm i @microsoft/signalr`.
2. Tạo service kết nối, lấy token từ Redux store (`store.getState().auth.accessToken`) qua `accessTokenFactory`, bật `withAutomaticReconnect()`.
3. Trong [`Dashboard.jsx`](../src/features/Dashboard/presentation/Dashboard.jsx): `connection.on("overviewChanged", ...)` ⇒ gọi lại `overViewApi.getOverViewById(type)` và `overViewApi.getInventoryActivityStats(type)` cho `timeRangeOption` đang chọn, cập nhật state biểu đồ.
4. `connection.stop()` khi unmount; fallback polling chu kỳ dài (~60s) khi `connection.state !== "Connected"`.
5. Khi token được refresh (`tokensRefreshed`), để `withAutomaticReconnect` tự lấy token mới ở lần reconnect; nếu đang mở mà bị đóng do 401 thì `start()` lại.

FE **chỉ cần** Backend chốt: **hub URL**, **tên event `overviewChanged`**, và **payload là tín hiệu (không chứa số liệu)**.
