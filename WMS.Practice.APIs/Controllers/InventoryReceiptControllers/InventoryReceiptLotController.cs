namespace WMS.Practice.APIs.Controllers.InventoryReceiptControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class InventoryReceiptLotController : ApiControllerBase
    {

        public InventoryReceiptLotController(IMediator mediator) : base(mediator)
        {
        }

        // API for InventoryReceiptLot

        [HttpGet("GetAllReceiptLots")]
        public async Task<IActionResult> GetAllReceiptLots()
        {
            var query = new GetAllReceiptLotsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetAllReceiptLotIds")]
        public async Task<IActionResult> GetAllReceiptLotIds()
        {
            var query = new GetAllReceiptLotIdsQuery();

            return await RequestAsync(query);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetReceiptLotByNotDone")]
        public async Task<IActionResult> GetReceiptLotByNotDone(string warehouseId = "TP01")
        {
            var query = new GetReceiptLotByNotDoneQuery(warehouseId);

            return await RequestAsync(query);
        }

        [HttpGet("GetReceiptLotById/{receiptLotId}")]
        public async Task<IActionResult> GetReceiptLotById(string receiptLotId)
        {
            var query = new GetReceiptLotByIdQuery(receiptLotId);

            return await RequestAsync(query);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPut("UpdateReceiptLotStatus")]
        public async Task<IActionResult> UpdateReceiptLotStatus([FromBody] UpdateReceiptLotStatusCommand command)
        {
            return await RequestAsync(command);
        }
    }
}
