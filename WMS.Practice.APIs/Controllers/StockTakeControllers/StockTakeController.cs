namespace WMS.Practice.APIs.Controllers.StockTakeControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class StockTakeController : ApiControllerBase
    {
        public StockTakeController(IMediator mediator) : base(mediator)
        {
        }

        // API for StockTake

        [HttpGet("GetAllStockTakes")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllStockTakeLotsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetStockTakeById/{stockTakeId}")]
        public async Task<IActionResult> GetById(string stockTakeId)
        {
            var query = new GetStockTakeByIdQuery(stockTakeId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateNewStockTake")]
        public async Task<IActionResult> Create([FromBody] CreateStockTakeCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateStockTakeCommand")]
        public async Task<IActionResult> Update([FromBody] UpdateStockTakeCommand command)
        {
            return await RequestAsync(command);
        }
    }
}
