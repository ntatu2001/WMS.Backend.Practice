namespace WMS.Practice.APIs.Controllers.InventoryReceiptControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryReceiptEntryController : ApiControllerBase
    {
        public InventoryReceiptEntryController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryReceiptEntry
        [HttpGet("GetAllReceiptEntries")]
        public async Task<IActionResult> GetAllReceiptEntries()
        {
            var query = new GetAllInventoryReceiptEntriesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetReceiptEntryById/{receiptEntryId}")]
        public async Task<IActionResult> GetReceiptEntryById(string receiptEntryId)
        {
            var query = new GetInventoryReceiptEntryByIdQuery(receiptEntryId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateInventoryReceiptEntry")]
        public async Task<IActionResult> CreateInventoryReceiptEntry([FromBody] CreateInventoryReceiptEntryCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPost("CreateInventoryReceiptEntries")]
        public async Task<IActionResult> CreateInventoryReceiptEntries([FromBody] CreateInventoryReceiptEntriesCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateInventoryReceiptEntry")]
        public async Task<IActionResult> UpdateInventoryReceiptEntry([FromBody] UpdateInventoryReceiptEntryCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteInventoryReceiptEntries")]
        public async Task<IActionResult> DeleteInventoryReceiptEntries([FromBody] DeleteInventoryReceiptEntriesCommand command)
        {
            return await RequestAsync(command);
        }
    }
}
