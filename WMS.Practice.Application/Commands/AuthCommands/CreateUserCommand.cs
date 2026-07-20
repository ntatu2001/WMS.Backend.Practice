namespace WMS.Practice.Application.Commands.AuthCommands
{
    public class CreateUserCommand : IRequest<CreateUserResultDTO>
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public string? EmployeeId { get; set; }
    }
}
