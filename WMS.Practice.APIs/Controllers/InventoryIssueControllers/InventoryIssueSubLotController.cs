namespace WMS.Practice.APIs.Controllers.InventoryIssueControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryIssueSubLotController : ApiControllerBase
    {
        public InventoryIssueSubLotController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryIssueSubLot

        [HttpGet("GetAllIssueSubLots")]
        public async Task<IActionResult> GetAllIssueSubLots()
        {
            var query = new GetAllIssueSubLotsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetIssueSubLotById/{issueSublotId}")]
        public async Task<IActionResult> GetIssueSubLotById(string issueSublotId)
        {
            var query = new GetIssueSubLotByIdQuery(issueSublotId);

            return await RequestAsync(query);
        }

        [HttpPut("UpdateIssueSubLot")]
        public async Task<IActionResult> UpdateIssueSubLot([FromBody] UpdateIssueSubLotsCommand command)
        {
            return await RequestAsync(command);
        }
    }
}
