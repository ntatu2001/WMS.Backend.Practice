namespace WMS.Practice.APIs.Controllers.PersonControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class CustomerController : ApiControllerBase
    {

        public CustomerController(IMediator mediator) : base(mediator)
        {
        }

        // API for Customer

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCustomersQuery();

            return await RequestAsync(query);
        }


        [HttpGet("GetCustomerById/{customerId}")]
        public async Task<IActionResult> GetById(string customerId)
        {
            var query = new GetCustomerByIdQuery(customerId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateNewCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpDelete("DeleteCustomer/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            var request = new DeleteCustomerCommand(customerId);

            return await RequestAsync(request);
        }
    }
}
