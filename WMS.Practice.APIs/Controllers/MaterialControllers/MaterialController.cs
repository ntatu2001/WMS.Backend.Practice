namespace WMS.Practice.APIs.Controllers.MaterialControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class MaterialController : ApiControllerBase
    {

        public MaterialController(IMediator mediator) : base(mediator)
        {
        }

        // API for Material 

        [HttpGet("GetAllMaterials")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int itemsPerPage)
        {
            var query = new GetAllMaterialQuery(page: pageNumber,
                                                itemsPerPage: itemsPerPage);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialsByClassIdQuery/{classId}")]
        public async Task<IActionResult> GetMaterialsByClassId(string classId)
        {
            var query = new GetMaterialsByClassIdQuery(classId);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialsByWarehouseId/{warehouseId}")]
        public async Task<IActionResult> GetMaterialsByWarehouseId(string warehouseId)
        {
            var query = new GetMaterialsByWarehouseIdQuery(warehouseId);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialsByWarehouseIdAndMaterialLot/{warehouseId}")]
        public async Task<IActionResult> GetMaterialsByWarehouseIdAndMaterialLot(string warehouseId)
        {
            var query = new GetMaterialsByWarehouseIdAndMaterialLotQuery(warehouseId);

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialById/{materialId}")]
        public async Task<IActionResult> GetById(string materialId)
        {
            var query = new GetMaterialByIdQuery(materialId);

            return await RequestAsync(query);
        }

        [HttpGet("GetUnitByMaterialId/{materialId}")]
        public async Task<IActionResult> GetUnitByMaterialId(string materialId)
        {
            var query = new GetUnitByMaterialIdQuery(materialId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateMaterial")]
        public async Task<IActionResult> CreateMaterial([FromBody] CreateMaterialCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateMaterial")]
        public async Task<IActionResult> UpdateMaterial([FromBody] UpdateMaterialCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteMaterial/{materialId}")]
        public async Task<IActionResult> DeleteMaterial(string materialId)
        {
            var command = new DeleteMaterialCommand(materialId);
            return await RequestAsync(command);
        }


        // API for MaterialProperty

        [HttpGet("GetAllMaterialProperties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var query = new GetAllMaterialPropertiesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetMaterialPropertyById/{propertyId}")]
        public async Task<IActionResult> GetPropertyById(string propertyId)
        {
            var query = new GetMaterialPropertyByIdQuery(propertyId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateMaterialProperty")]
        public async Task<IActionResult> CreateMaterialProperty([FromBody] CreateMaterialPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateMaterialProperty")]
        public async Task<IActionResult> UpdateMaterialProperty([FromBody] UpdateMaterialPropertyCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteMaterialProperty/{propertyId}")]
        public async Task<IActionResult> DeleteMaterialProperty(string propertyId)
        {
            var command = new DeleteMaterialPropertyCommand(propertyId);
            return await RequestAsync(command);
        }

    }
}
