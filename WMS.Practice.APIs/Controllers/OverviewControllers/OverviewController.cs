namespace WMS.Practice.APIs.Controllers.OverviewControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    [Authorize(Roles = "Admin")]
    public class OverviewController : ApiControllerBase
    {
        public OverviewController(IMediator mediator) : base(mediator)
        {
        }

        // API for Overview

        [HttpGet("GetInventoryActivityStats")]
        public async Task<IActionResult> GetInventoryActivityStats(string timeRangeOption = "Today")
        {
            if (!Enum.TryParse<TimeRangeOption>(timeRangeOption, true, out var option))
            {
                option = TimeRangeOption.Today;
            }

            var query = new GetInventoryActivityStatsQuery(option);

            return await RequestAsync(query);
        }

        [HttpGet("GetWarehouseInventoryMovementStats")]
        public async Task<IActionResult> GetInventoryActivityStatsByLocation(string timeRangeOption = "Today")
        {
            if (!Enum.TryParse<TimeRangeOption>(timeRangeOption, true, out var option))
            {
                option = TimeRangeOption.Today;
            }

            var query = new GetWarehouseInventoryMovementStatsQuery(option);

            return await RequestAsync(query);
        }
    }
}
