namespace WMS.Practice.APIs.Controllers.InventoryIssueControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryIssueController : ApiControllerBase
    {

        public InventoryIssueController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryIssue

        [HttpGet("GetAllIssues")]
        public async Task<IActionResult> GetAllIssue()
        {
            var query = new GetAllInventoryIssuesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetIssueById/{InventoryIssueId}")]
        public async Task<IActionResult> GetIssueById(string InventoryIssueId)
        {
            var query = new GetInventoryIssueByIdQuery(InventoryIssueId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateInventoryIssue")]
        public async Task<IActionResult> CreateIssue([FromBody] CreateInventoryIssueCommand command)
        {
            return await RequestAsync(command);

        }

        [HttpPut("UpdateInventoryIssue")]
        public async Task<IActionResult> UpdateIssue([FromBody] UpdateInventoryIssueCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteInventoryIssue/{IssueId}")]
        public async Task<IActionResult> DeleteIssue(string IssueId)
        {
            var command = new DeleteInventoryIssueCommand(IssueId);
            return await RequestAsync(command);
        }
    }
}
