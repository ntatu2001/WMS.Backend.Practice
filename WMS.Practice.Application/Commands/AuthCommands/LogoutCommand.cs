namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class LogoutCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
