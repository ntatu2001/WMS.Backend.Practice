namespace WMS.Practice.APIs.Controllers.StorageControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class WarehouseController : ApiControllerBase
    {
        public WarehouseController(IMediator mediator) : base(mediator)
        {
        }

        // API for Warehouse

        [HttpGet("GetAllWarehouses")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllWarehouseQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetWarehouseIdByWarehouseName/{warehouseName}")]
        public async Task<IActionResult> GetWarehouseIdByWarehouseName(string warehouseName)
        {
            var query = new GetWarehouseIdByWarehouseNameQuery(warehouseName);

            return await RequestAsync(query);
        }

        [HttpGet("GetWarehouseById/{warehouseId}")]
        public async Task<IActionResult> GetById(string warehouseId)
        {
            var query = new GetWarehouseByIdQuery(warehouseId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateNewWarehouse")]
        public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpPut("UpdateWarehouse")]
        public async Task<IActionResult> UpdateWarehouse([FromBody] UpdateWarehouseCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpDelete("DeleteWarehouse/{warehouseId}")]
        public async Task<IActionResult> DeleteWarehouse(string warehouseId)
        {
            var request = new DeleteWarehouseCommand(warehouseId);

            return await RequestAsync(request);
        }


        // API for Warehouse Property

        [HttpPost("Create New Warehouse Property")]
        public async Task<IActionResult> CreateWarehouseProperty([FromBody] CreateWarehousePropertyCommand request)
        {
            return await RequestAsync(request);
        }
    }
}
