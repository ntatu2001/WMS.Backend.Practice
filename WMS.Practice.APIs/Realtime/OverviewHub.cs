namespace WMS.Practice.APIs.Realtime
{
    /// <summary>
    /// Hub một chiều (server -> client) cho màn hình Tổng quan.
    /// Server chỉ phát event <c>overviewChanged</c>; client không gọi method nào lên.
    /// Dùng đúng JWT Bearer như REST (token truyền qua query string <c>?access_token=</c>
    /// cho path <c>/WarehouseAPI/hubs</c>, xem cấu hình JwtBearerEvents trong Program.cs).
    /// </summary>
    [Authorize(Roles = "Admin")]
    public sealed class OverviewHub : Hub
    {
        private readonly ILogger<OverviewHub> _logger;

        public OverviewHub(ILogger<OverviewHub> logger) => _logger = logger;

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Overview client connected: {ConnectionId} (user {User})",
                Context.ConnectionId, Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation("Overview client disconnected: {ConnectionId}", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
