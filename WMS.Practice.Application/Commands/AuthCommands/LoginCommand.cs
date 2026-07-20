namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class LoginCommand : IRequest<AuthResultDTO>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
