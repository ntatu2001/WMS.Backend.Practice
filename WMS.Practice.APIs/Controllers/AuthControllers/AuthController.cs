namespace WMS.Practice.APIs.Controllers.AuthControllers
{
    [ApiController]
    [Route("WarehouseAPI/[controller]")]
    public class AuthController : ApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            return await RequestAsync(request);
        }

        [AllowAnonymous]
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand request)
        {
            return await RequestAsync(request);
        }

        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand request)
        {
            return await RequestAsync(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request)
        {
            return await RequestAsync(request);
        }
    }
}
