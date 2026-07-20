namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly ITokenService _tokenService;

        public LogoutCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _tokenService.RevokeRefreshTokenAsync(request.RefreshToken, cancellationToken);
            return true;
        }
    }
}
