namespace WMS.Practice.APIs.Controllers.PersonControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class SupplierController : ApiControllerBase
    {
        public SupplierController(IMediator mediator) : base(mediator)
        {
        }

        // API for Supplier

        [HttpGet("GetAllSupplier")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSuppliersQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetSupplierById/{supplierId}")]
        public async Task<IActionResult> GetById(string supplierId)
        {
            var query = new GetSupplierByIdQuery(supplierId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateNewSupplier")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpPut("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierCommand command)
        {
            return await RequestAsync(command);
        }

        [HttpDelete("DeleteSupplier/{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(string supplierId)
        {
            var request = new DeleteSupplierCommand(supplierId);

            return await RequestAsync(request);
        }
    }
}
