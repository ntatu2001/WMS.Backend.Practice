namespace WMS.Practice.APIs.Identity
{
    public static class IdentitySeeder
    {
        private static readonly string[] DefaultRoles = { "Admin", "Manager", "Staff" };

        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

            foreach (var roleName in DefaultRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new AppRole { Name = roleName });
                }
            }
        }
    }
}
