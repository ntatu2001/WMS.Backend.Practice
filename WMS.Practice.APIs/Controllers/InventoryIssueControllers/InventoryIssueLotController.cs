namespace WMS.Practice.APIs.Controllers.InventoryIssueControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryIssueLotController : ApiControllerBase
    {

        public InventoryIssueLotController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryIssueLot

        [HttpGet("GetAllIssueLots")]
        public async Task<IActionResult> GetAllIssueLots()
        {
            var query = new GetAllIssueLotsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetIssueLotsNotDone")]
        public async Task<IActionResult> GetIssueLotsNotDone(string warehouseId = "TP01")
        {
            var query = new GetIssueLotByNotDoneQuery(warehouseId);

            return await RequestAsync(query);
        }

        [HttpGet("GetIssueLotById/{IssueLotId}")]
        public async Task<IActionResult> GetIssueLotById(string IssueLotId)
        {
            var query = new GetIssueLotByIdQuery(IssueLotId);

            return await RequestAsync(query);
        }

        [HttpPut("UpdateIssueLotStatus")]
        public async Task<IActionResult> UpdateIssueLotStatus([FromBody] UpdateIssueLotStatusCommand command)
        {
            return await RequestAsync(command);
        }
    }
}
