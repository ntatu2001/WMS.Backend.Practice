namespace WMS.Practice.APIs.Controllers.MaterialControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class MaterialSubLotController : ApiControllerBase
    {
        public MaterialSubLotController(IMediator mediator) : base(mediator)
        {
        }

        // API for MaterialSubLot

        [HttpGet("GetAllMaterialSubLot")]
        public async Task<IActionResult> GetAllMaterialSubLot()
        {
            var query = new GetAllMaterialSubLotsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialSubLotsByLocationId/{LocationId}")]
        public async Task<IActionResult> GetMaterialSubLotsByLocationId(string LocationId)
        {
            var query = new GetMaterialSubLotsByLocationIdQuery(LocationId);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialSubLotsByLotNumber/{lotNumber}")]
        public async Task<IActionResult> GetMaterialSubLotsByLotNumber(string lotNumber)
        {
            var query = new GetMaterialSubLotsByLotNumberQuery(lotNumber);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialSubLotsByStatus/{status}")]
        public async Task<IActionResult> GetMaterialSubLotsByStatus(string status)
        {
            var query = new GetMaterialSubLotsByStatusQuery(status);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialSubLotById/{sublotId}")]
        public async Task<IActionResult> GetMaterialSubLotById(string sublotId)
        {
            var query = new GetMaterialSubLotByIdQuery(sublotId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateMaterialSubLot")]
        public async Task<IActionResult> CreateMaterialSubLot([FromBody] CreateMaterialSubLotCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateMaterialSubLot")]
        public async Task<IActionResult> UpdateMaterialSubLot([FromBody] UpdateMaterialSubLotsCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateMaterialSubLotQuantity")]
        public async Task<IActionResult> UpdateMaterialSubLotQuantity([FromBody] UpdateMaterialSubLotQuantityCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteMaterialSubLot/{sublotId}")]
        public async Task<IActionResult> DeleteMaterialSubLot(string sublotId)
        {
            var command = new DeleteMaterialSubLotCommand(sublotId);
            return await RequestAsync(command);
        }
    }
}
