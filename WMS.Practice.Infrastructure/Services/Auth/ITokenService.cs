namespace WMS.Practice.Infrastructure.Services.Auth
{
    public interface ITokenService
    {
        (string AccessToken, DateTime ExpiresAt) GenerateAccessToken(AppUser user, IList<string> roles);
        Task<(string RawRefreshToken, DateTime ExpiresAt)> GenerateAndStoreRefreshTokenAsync(string userId, CancellationToken cancellationToken = default);
        Task<AppUser?> ValidateRefreshTokenAsync(string rawRefreshToken, CancellationToken cancellationToken = default);
        Task<(string RawRefreshToken, DateTime ExpiresAt)> RotateRefreshTokenAsync(string oldRawRefreshToken, CancellationToken cancellationToken = default);
        Task RevokeRefreshTokenAsync(string rawRefreshToken, CancellationToken cancellationToken = default);
    }
}
