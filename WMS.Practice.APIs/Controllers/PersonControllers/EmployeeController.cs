namespace WMS.Practice.APIs.Controllers.PersonControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class EmployeeController : ApiControllerBase
    {
        public EmployeeController(IMediator mediator) : base(mediator)
        {
        }

        // API for Employee

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllEmployeesQuery();

            return await RequestAsync(query);

        }

        [HttpGet("GetEmployeeById/{personId}")]
        public async Task<IActionResult> GetById(string personId)
        {
            var query = new GetEmployeeByIdQuery(personId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateNewEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpDelete("DeleteEmployee/{personId}")]
        public async Task<IActionResult> DeleteEmployee(string personId)
        {
            var request = new DeleteEmployeeCommand(personId);

            return await RequestAsync(request);
        }

        // API for Employee Property

        [HttpGet("GetAllEmployeeProperties")]
        public async Task<IActionResult> GetAllEmployeeProperties()
        {
            var query = new GetAllEmployeePropertiesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetEmployeePropertyById/{propertyId}")]
        public async Task<IActionResult> GetEmployeePropertyById(string propertyId)
        {
            var query = new GetEmployeePropertyByIdQuery(propertyId);

            return await RequestAsync(query);
        }

        [HttpPost("CreateNewEmployeeProperty")]
        public async Task<IActionResult> CreateEmployeeProperty([FromBody] CreateEmployeePropertyCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpPut("UpdateEmployeeProperty")]
        public async Task<IActionResult> UpdateEmployeeProperty([FromBody] UpdateEmployeePropertyCommand request)
        {
            return await RequestAsync(request);
        }

        [HttpDelete("DeleteEmployeeProperty/{propertyId}")]
        public async Task<IActionResult> DeleteEmployeeProperty(string propertyId)
        {
            var request = new DeleteEmployeePropertyCommand(propertyId);

            return await RequestAsync(request);
        }
    }
}
