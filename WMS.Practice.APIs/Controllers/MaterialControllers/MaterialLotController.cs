namespace WMS.Practice.APIs.Controllers.MaterialControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class MaterialLotController : ApiControllerBase
    {
        public MaterialLotController(IMediator mediator) : base(mediator)
        {
        }

        // API for MaterialLot

        [HttpGet("GetAllMaterialLots")]
        public async Task<IActionResult> GetAllMaterialLots()
        {
            var query = new GetAllMaterialLotsQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialLotsByMaterialId/{materialId}")]
        public async Task<IActionResult> GetMaterialLotsByMaterialId(string materialId)
        {
            var query = new GetMaterialLotsByMaterialIdQuery(materialId);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialLotbyLotStatus/{status}")]
        public async Task<IActionResult> GetMaterialLotsByStatus(string status)
        {
            var query = new GetMaterialLotsByStatusQuery(status);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialLotById/{lotNumber}")]
        public async Task<IActionResult> GetMaterialLotById(string lotNumber)
        {
            var query = new GetMaterialLotByIdQuery(lotNumber);

            return await RequestAsync(query);
        }

        [HttpGet("GetQuantityByMaterialLotId/{lotNumber}")]
        public async Task<IActionResult> GetQuantityByMaterialLotId(string lotNumber)
        {
            var query = new GetQuantityByMaterialLotIdQuery(lotNumber);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialLotsByWarehouseId/{warehouseId}")]
        public async Task<IActionResult> GetMaterialLotsByWarehouseId(string warehouseId)
        {
            var query = new GetMaterialLotsByWarehouseIdQuery(warehouseId);

            return await RequestAsync(query);
        }


        [HttpPost("CreateMaterialLot")]
        public async Task<IActionResult> CreateMaterialLot([FromBody] CreateMaterialLotCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateInventoryLogForIssue")]
        public async Task<IActionResult> UpdateMaterialLot([FromBody] UpdateMaterialLotCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateQuantityMaterialLots")]
        public async Task<IActionResult> UpdateQuantityMaterialLots([FromBody] UpdateMaterialLotQuantityCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteMaterialLot/{lotNumber}")]
        public async Task<IActionResult> DeleteMaterialLot(string lotNumber)
        {
            var command = new DeleteMaterialLotCommand(lotNumber);
            return await RequestAsync(command);
        }

        // API for MaterialLotProperty

        [HttpGet("GetAllMaterialLotProperties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var query = new GetAllMaterialLotPropertiesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialLotPropertyById/{propertyId}")]
        public async Task<IActionResult> GetMaterialLotPropertyById(string propertyId)
        {
            var query = new GetMaterialLotPropertyByIdQuery(propertyId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateMaterialLotProperty")]
        public async Task<IActionResult> CreateMaterialLotProperty([FromBody] CreateMaterialLotPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateMaterialLotProperty")]
        public async Task<IActionResult> UpdateMaterialLotProperty([FromBody] UpdateMaterialLotPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteMaterialLotProperty/{propertyId}")]
        public async Task<IActionResult> DeleteMaterialLotProperty(string propertyId)
        {
            var command = new DeleteMaterialLotPropertyCommand(propertyId);
            return await RequestAsync(command);
        }
    }
}
