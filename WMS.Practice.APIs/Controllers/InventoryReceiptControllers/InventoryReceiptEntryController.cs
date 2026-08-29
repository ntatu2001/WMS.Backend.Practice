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
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetReceiptEntriesNotPendingByDate")]
        public async Task<IActionResult> GetReceiptEntriesNotPendingByDate(DateTime? fromDate = null, DateTime? toDate = null, string? warehouseName = null,
                                                                   int? pageNumber = null, int? pageSize = null)
        {
            var query = new GetInventoryReceiptEntriesQuery(fromDate: fromDate, toDate: toDate, warehouseName: warehouseName,
                                                              pageNumber: pageNumber, pageSize: pageSize);

            return await RequestAsync(query);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("GetReceiptEntriesByLotNumber")]
        public async Task<IActionResult> GetReceiptEntriesByLotNumber(string? lotNumber = null, string? materialName = null, string? warehouseName = null,
                                                                        int? pageNumber = null, int? pageSize = null)
        {
            var query = new GetInventoryReceiptEntriesQuery(warehouseName: warehouseName, lotNumber: lotNumber, materialName: materialName,
                                                              pageNumber: pageNumber, pageSize: pageSize);

            return await RequestAsync(query);
        }

        [Authorize(Roles = "Manager,Admin")]
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
