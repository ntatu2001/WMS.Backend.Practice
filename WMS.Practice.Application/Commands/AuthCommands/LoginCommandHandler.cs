namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDTO>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResultDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new InvalidCredentialsException();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var (accessToken, accessTokenExpiresAt) = _tokenService.GenerateAccessToken(user, roles);
            var (refreshToken, _) = await _tokenService.GenerateAndStoreRefreshTokenAsync(user.Id, cancellationToken);

            return new AuthResultDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAtUtc = accessTokenExpiresAt
            };
        }
    }
}
