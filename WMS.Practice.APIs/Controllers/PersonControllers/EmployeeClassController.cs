namespace WMS.Practice.APIs.Controllers.PersonControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class EmployeeClassController : ApiControllerBase
    {
        public EmployeeClassController(IMediator mediator) : base(mediator)
        {
        }

        // API for EmployeeClass

        [HttpGet("GetAllEmployeeClasses")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllEmployeeClassesQuery();

            return await RequestAsync(query);
        }

        [HttpGet("GetEmployeeClassById/{employeeClassId}")]
        public async Task<IActionResult> GetById(string employeeClassId)
        {
            var query = new GetEmployeeClassByIdQuery(employeeClassId);

            return await RequestAsync(query);
        }
    }
}
