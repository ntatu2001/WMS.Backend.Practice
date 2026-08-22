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
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int itemsPerPage)
        {
            var query = new GetAllEmployeesQuery(page: pageNumber, itemsPerPage: itemsPerPage);

            return await RequestAsync(query);

        }

        [HttpGet("SearchEmployeesByEmployeeId")]
        public async Task<IActionResult> SearchEmployeesByEmployeeId([FromQuery] string? employeeId, [FromQuery] string? employeeClassId,
                                                                       [FromQuery] int pageNumber, [FromQuery] int itemsPerPage)
        {
            var query = new SearchEmployeesByEmployeeIdQuery(employeeId: employeeId, employeeClassId: employeeClassId,
                                                               page: pageNumber, itemsPerPage: itemsPerPage);

            return await RequestAsync(query);
        }

        [HttpGet("GetAllEmployeeNameId")]
        public async Task<IActionResult> GetAllEmployeeNameId()
        {
            var query = new GetAllEmployeeNameIdQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetEmployeeById/{employeeId}")]
        public async Task<IActionResult> GetById(string employeeId)
        {
            var query = new GetEmployeeByIdQuery(employeeId);

            return await RequestAsync(query);
        }

        [Authorize(Roles = "Admin")]
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

        [HttpDelete("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(string employeeId)
        {
            var request = new DeleteEmployeeCommand(employeeId);

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
