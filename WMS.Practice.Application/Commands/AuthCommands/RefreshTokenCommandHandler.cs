namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResultDTO>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResultDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _tokenService.ValidateRefreshTokenAsync(request.RefreshToken, cancellationToken)
                       ?? throw new InvalidRefreshTokenException();

            (string RawRefreshToken, DateTime ExpiresAt) rotatedToken;
            try
            {
                rotatedToken = await _tokenService.RotateRefreshTokenAsync(request.RefreshToken, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidRefreshTokenException(ex.Message, ex);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var (accessToken, accessTokenExpiresAt) = _tokenService.GenerateAccessToken(user, roles);

            return new AuthResultDTO
            {
                AccessToken = accessToken,
                RefreshToken = rotatedToken.RawRefreshToken,
                AccessTokenExpiresAtUtc = accessTokenExpiresAt
            };
        }
    }
}
