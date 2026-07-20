namespace WMS.Practice.Application.DTOs.AuthDTOs
{
    public class AuthResultDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiresAtUtc { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
