namespace WMS.Practice.APIs.Identity
{
    public static class IdentitySeeder
    {
        private static readonly string[] DefaultRoles = { "Admin", "Manager", "Staff" };

        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            foreach (var roleName in DefaultRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new AppRole { Name = roleName });
                }
            }

            var adminSection = configuration.GetSection("InitialAdmin");
            var adminUserName = adminSection["UserName"];
            var adminEmail = adminSection["Email"];
            var adminPassword = adminSection["Password"];

            if (string.IsNullOrEmpty(adminUserName) || string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            {
                return;
            }

            if (await userManager.FindByNameAsync(adminUserName) is not null)
            {
                return;
            }

            var adminUser = new AppUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (createResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                logger.LogWarning("Seeded initial Admin account '{UserName}' with a development placeholder password. Change it before using this environment beyond local development.", adminUserName);
            }
        }
    }
}
