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

        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetIssueEntriesByDate")]
        public async Task<IActionResult> GetIssueEntriesByDate(DateTime? fromDate = null, DateTime? toDate = null, string? warehouseName = null,
                                                                 int? pageNumber = null, int? pageSize = null)
        {
            var query = new GetInventoryIssueEntriesQuery(fromDate: fromDate, toDate: toDate, warehouseName: warehouseName,
                                                            pageNumber: pageNumber, pageSize: pageSize);

            return await RequestAsync(query);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetIssueEntriesByLotNumber")]
        public async Task<IActionResult> GetIssueEntriesByLotNumber(string? lotNumber = null, string? materialName = null, string? warehouseName = null,
                                                                      int? pageNumber = null, int? pageSize = null)
        {
            var query = new GetInventoryIssueEntriesQuery(warehouseName: warehouseName, lotNumber: lotNumber, materialName: materialName,
                                                            pageNumber: pageNumber, pageSize: pageSize);

            return await RequestAsync(query);
        }

        [Authorize(Roles = "Manager,Admin")]
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
