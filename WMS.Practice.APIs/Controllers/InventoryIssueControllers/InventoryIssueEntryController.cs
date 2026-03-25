namespace WMS.Practice.APIs.Controllers.InventoryIssueControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryIssueEntryController : ApiControllerBase
    {

        public InventoryIssueEntryController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryIssueEntry

        [HttpGet("GetAllIssueEntries")]
        public async Task<IActionResult> GetAllIssueEntries()
        {
            var query = new GetAllInventoryIssueEntriesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetIssueEntryById/{IssueEntryId}")]
        public async Task<IActionResult> GetIssueEntryById(string IssueEntryId)
        {
            var query = new GetInventoryIssueEntryByIdQuery(IssueEntryId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateIssueEntry")]
        public async Task<IActionResult> CreateIssueEntry([FromBody] CreateInventoryIssueEntryCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateIssueEntry")]
        public async Task<IActionResult> UpdateIssueEntry([FromBody] UpdateInventoryIssueEntryCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteIssueEntries")]
        public async Task<IActionResult> DeleteIssueEntries([FromBody] DeleteInventoryIssueEntriesCommand command)
        {
            return await RequestAsync(command);
        }
    }
}
