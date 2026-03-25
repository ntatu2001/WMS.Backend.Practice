namespace WMS.Practice.APIs.Controllers.InventoryReceiptControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryReceiptController : ApiControllerBase
    {
        public InventoryReceiptController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryReceipt

        [HttpGet("GetAllReceipts")]
        public async Task<IActionResult> GetAllReceipts()
        {
            var query = new GetAllInventoryReceiptsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetReceiptById/{receiptId}")]
        public async Task<IActionResult> GetReceiptById(string receiptId)
        {
            var query = new GetInventoryReceiptByIdQuery(receiptId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateReceipt")]
        public async Task<IActionResult> CreateReceipt([FromBody] CreateInventoryReceiptCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateReceipt")]
        public async Task<IActionResult> UpdateReceipt([FromBody] UpdateInventoryReceiptCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteReceipt/{receiptId}")]
        public async Task<IActionResult> DeleteReceipt(string receiptId)
        {
            var command = new DeleteInventoryReceiptCommand(receiptId);
            return await RequestAsync(command);
        }
    }
}
