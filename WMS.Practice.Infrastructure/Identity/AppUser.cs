namespace WMS.Practice.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
