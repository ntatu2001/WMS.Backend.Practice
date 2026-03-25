namespace WMS.Practice.APIs.Controllers.InventoryReceiptControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryReceiptSublotController : ApiControllerBase
    {

        public InventoryReceiptSublotController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryReceiptSubLot

        [HttpGet("GetAllReceiptSublots")]
        public async Task<IActionResult> GetAllReceiptSublots()
        {
            var query = new GetAllReceiptSubLotsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetReceiptSubLotById/{receiptSublotId}")]
        public async Task<IActionResult> GetReceiptSubLotById(string receiptSublotId)
        {
            var query = new GetReceiptSubLotByIdQuery(receiptSublotId);

            return await RequestAsync(query);
        }

        [HttpPut("UpdateReceiptSublot")]
        public async Task<IActionResult> UpdateReceiptSublot([FromBody] UpdateReceiptSubLotsCommand command)
        {
            return await RequestAsync(command);
        }
    }
}
