namespace WMS.Practice.APIs.Controllers.MaterialControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class MaterialClassController : ApiControllerBase
    {
        public MaterialClassController(IMediator mediator) : base(mediator)
        {
        }

        // API for MaterialClass

        [HttpGet("GetAllMaterialClass")]
        public async Task<IActionResult> GetAllMaterialClass()
        {
            var query = new GetAllMaterialClassesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialClassById/{materialClassId}")]
        public async Task<IActionResult> GetById(string materialClassId)
        {
            var query = new GetMaterialClassByIdQuery(materialClassId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateMaterialClass")]
        public async Task<IActionResult> CreateMaterialClass([FromBody] CreateMaterialClassCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateMaterialClass")]
        public async Task<IActionResult> UpdateMaterialClass([FromBody] UpdateMaterialClassCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteMaterialClass/{materialClassId}")]
        public async Task<IActionResult> DeleteMaterialClass(string materialClassId)
        {
            var command = new DeleteMaterialClassCommand(materialClassId);
            return await RequestAsync(command);
        }


        // API for MaterialClassProperty

        [HttpGet("GetAllProperties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var query = new GetAllMaterialClassPropertiesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialClassPropertyById/{materialClassPropertyId}")]
        public async Task<IActionResult> GetPropertyById(string materialClassPropertyId)
        {
            var query = new GetMaterialClassPropertyByIdQuery(materialClassPropertyId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateMaterialClassProperty")]
        public async Task<IActionResult> CreateMaterialClassProperty([FromBody] CreateMaterialClassPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateMaterialClassProperty")]
        public async Task<IActionResult> UpdateMaterialClassProperty([FromBody] UpdateMaterialClassPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteMaterialClassProperty/{materialClassPropertyId}")]
        public async Task<IActionResult> DeleteMaterialClassProperty(string materialClassPropertyId)
        {
            var command = new DeleteMaterialClassPropertyCommand(materialClassPropertyId);
            return await RequestAsync(command);
        }

    }
}
