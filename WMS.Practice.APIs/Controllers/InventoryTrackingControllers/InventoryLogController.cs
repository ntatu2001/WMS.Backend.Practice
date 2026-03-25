namespace WMS.Practice.APIs.Controllers.InventoryTrackingControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryLogController : ApiControllerBase
    {

        public InventoryLogController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryLog

        [HttpGet("GetAllInventoryLogs")]
        public async Task<IActionResult> GetAllInventoryLogs(string transactionType = "Both")
        {
            var query = new GetAllInventoryLogsQuery(transactionType);

            return await RequestAsync(query);
        }

        [HttpGet("GetInventoryLogByLotNumber/{lotNumber}")]
        public async Task<IActionResult> GetInventoryLogByLotNumber(string lotNumber, string transactionType = "Both")
        {
            var query = new GetInventoryLogByLotNumberQuery(lotNumber, transactionType);

            return await RequestAsync(query);

        }

        [HttpGet("GetAllReceiptLotsTracking")]
        public async Task<IActionResult> GetAllReceiptLotsTracking(string? lotNumber, string? supplierId, DateTime? startTime, DateTime? endTime)
        {
            var query = new GetAllReceiptLotsTrackingQuery(lotNumber, supplierId, startTime, endTime);

            return await RequestAsync(query);
        }

        [HttpGet("GetAllIssueLotsTracking")]
        public async Task<IActionResult> GetAllIssueLotsTracking(string? lotNumber, string? customerId, DateTime? startTime, DateTime? endTime)
        {
            var query = new GetAllIssueLotsTrackingQuery(lotNumber, customerId, startTime, endTime);

            return await RequestAsync(query);
        }

        [HttpGet("GetLotAdjustmentsTracking")]
        public async Task<IActionResult> GetAllAdjustmentLotsTracking(string? lotNumber, string? materialId, DateTime? startTime, DateTime? endTime)
        {
            var query = new GetStockTakeLotTrackingQuery(lotNumber, materialId, startTime, endTime);

            return await RequestAsync(query);
        }
    }
}
