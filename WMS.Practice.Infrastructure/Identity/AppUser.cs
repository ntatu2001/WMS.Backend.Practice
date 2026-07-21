namespace WMS.Practice.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string EmployeeId { get; set; } = string.Empty;
        public Employee Employee { get; set; } = null!;
    }
}
