namespace WMS.Practice.APIs.Controllers.StorageControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class LocationController : ApiControllerBase
    {
        public LocationController(IMediator mediator) : base(mediator)
        {
        }

        // API for Location

        [HttpGet("GetAllLocations")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int itemsPerPage)
        {
            var query = new GetAllLocationQuery(pageNumber, itemsPerPage);

            return await RequestAsync(query);
        }

        [HttpGet("GetLocationsByWarehouseId/{warehouseId}")]
        public async Task<IActionResult> GetByWarehouseId(string warehouseId)
        {
            var query = new GetLocationsByWarehouseIdQuery(warehouseId);

            return await RequestAsync(query);
        }

        [HttpGet("GetLocationById/{locationId}")]
        public async Task<IActionResult> GetById(string locationId)
        {
            var query = new GetLocationByIdQuery(locationId);

            return await RequestAsync(query);
        }

        [HttpGet("GetInforByLocationId/{locationId}")]
        public async Task<IActionResult> GetInforByLocationId(string locationId)
        {
            var query = new GetStorageInfoByLocationIdQuery(locationId);

            return await RequestAsync(query);
        }

        [HttpGet("GetStockLocationHistoriesByLocationId")]
        public async Task<IActionResult> GetStockLocationHistoriesByLocationId(string? locationId, DateTime? startTime, DateTime? endTime)
        {
            var query = new GetLocationStorageHistoriesQuery(locationId, startTime, endTime);

            return await RequestAsync(query);
        }

        [HttpPost("CreateNewLocation")]
        public async Task<IActionResult> Create([FromBody] CreateLocationCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateLocation")]
        public async Task<IActionResult> Update([FromBody] UpdateLocationCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteLocation/{locationId}")]
        public async Task<IActionResult> Delete(string locationId)
        {
            var command = new DeleteLocationCommand(locationId);

            return await RequestAsync(command);
        }

        // API for Location Property

        [HttpGet("GetAllLocationProperties")]
        public async Task<IActionResult> GetAllLocationProperties()
        {
            var query = new GetAllLocationPropertiesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetLocationPropertyById/{locationPropertyId}")]
        public async Task<IActionResult> GetLocationPropertyById(string locationPropertyId)
        {
            var query = new GetLocationPropertyByIdQuery(locationPropertyId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateLocationProperty")]
        public async Task<IActionResult> CreateLocationProperty([FromBody] CreateLocationPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateLocationProperty")]
        public async Task<IActionResult> UpdateLocationProperty([FromBody] UpdateLocationPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteLocationProperty/{locationPropertyId}")]
        public async Task<IActionResult> DeleteLocationProperty(string locationPropertyId)
        {
            var command = new DeleteLocationPropertyCommand(locationPropertyId);

            return await RequestAsync(command);
        }

    }
}
