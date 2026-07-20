namespace WMS.Practice.Application.DTOs.AuthDTOs
{
    public class CreateUserResultDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public string? EmployeeId { get; set; }
    }
}
