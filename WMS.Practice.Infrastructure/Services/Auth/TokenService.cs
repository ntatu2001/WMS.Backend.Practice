namespace WMS.Practice.Infrastructure.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly WMSDbContext _context;
        private readonly JwtSettings _settings;

        public TokenService(WMSDbContext context, IOptions<JwtSettings> options)
        {
            _context = context;
            _settings = options.Value;
        }

        public (string AccessToken, DateTime ExpiresAt) GenerateAccessToken(AppUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            if (!string.IsNullOrEmpty(user.EmployeeId))
            {
                claims.Add(new Claim("employeeId", user.EmployeeId));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SigningKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpiryMinutes);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }

        public async Task<(string RawRefreshToken, DateTime ExpiresAt)> GenerateAndStoreRefreshTokenAsync(string userId, CancellationToken cancellationToken = default)
        {
            var rawToken = GenerateRawToken();
            var expiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpiryDays);

            var refreshToken = new RefreshToken
            {
                TokenHash = HashToken(rawToken),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return (rawToken, expiresAt);
        }

        public async Task<AppUser?> ValidateRefreshTokenAsync(string rawRefreshToken, CancellationToken cancellationToken = default)
        {
            var tokenHash = HashToken(rawRefreshToken);
            var refreshToken = await _context.RefreshTokens
                                              .Include(t => t.User)
                                              .FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);

            return refreshToken is not null && refreshToken.IsActive ? refreshToken.User : null;
        }

        public async Task<(string RawRefreshToken, DateTime ExpiresAt)> RotateRefreshTokenAsync(string oldRawRefreshToken, CancellationToken cancellationToken = default)
        {
            var oldTokenHash = HashToken(oldRawRefreshToken);
            var oldRefreshToken = await _context.RefreshTokens
                                                 .FirstOrDefaultAsync(t => t.TokenHash == oldTokenHash, cancellationToken);

            if (oldRefreshToken is null)
            {
                throw new InvalidOperationException("Refresh token not found.");
            }

            if (!oldRefreshToken.IsActive)
            {
                if (oldRefreshToken.RevokedAt is not null)
                {
                    await RevokeAllActiveTokensForUserAsync(oldRefreshToken.UserId, cancellationToken);
                }

                throw new InvalidOperationException("Refresh token is no longer active.");
            }

            var (newRawToken, newExpiresAt) = (GenerateRawToken(), DateTime.UtcNow.AddDays(_settings.RefreshTokenExpiryDays));
            var newTokenHash = HashToken(newRawToken);

            oldRefreshToken.RevokedAt = DateTime.UtcNow;
            oldRefreshToken.ReplacedByTokenHash = newTokenHash;

            _context.RefreshTokens.Add(new RefreshToken
            {
                TokenHash = newTokenHash,
                UserId = oldRefreshToken.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = newExpiresAt
            });

            await _context.SaveChangesAsync(cancellationToken);

            return (newRawToken, newExpiresAt);
        }

        public async Task RevokeRefreshTokenAsync(string rawRefreshToken, CancellationToken cancellationToken = default)
        {
            var tokenHash = HashToken(rawRefreshToken);
            var refreshToken = await _context.RefreshTokens
                                              .FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);

            if (refreshToken is null || refreshToken.RevokedAt is not null)
            {
                return;
            }

            refreshToken.RevokedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task RevokeAllActiveTokensForUserAsync(string userId, CancellationToken cancellationToken)
        {
            var activeTokens = await _context.RefreshTokens
                                              .Where(t => t.UserId == userId && t.RevokedAt == null)
                                              .ToListAsync(cancellationToken);

            foreach (var token in activeTokens)
            {
                token.RevokedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private static string GenerateRawToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes)
                           .Replace('+', '-')
                           .Replace('/', '_')
                           .TrimEnd('=');
        }

        private static string HashToken(string rawToken)
        {
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
