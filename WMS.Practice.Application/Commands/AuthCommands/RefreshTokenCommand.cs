namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class RefreshTokenCommand : IRequest<AuthResultDTO>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
