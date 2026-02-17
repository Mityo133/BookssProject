using Microsoft.AspNetCore.Identity;

namespace Bookss.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAdminAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            string adminEmail = configuration["AdminSettings:Email"];
            string adminPassword = configuration["AdminSettings:Password"];

            Console.WriteLine($"🔧 Starting admin seeder...");
            Console.WriteLine($"📧 Admin Email: {adminEmail}");
            Console.WriteLine($"🔑 Password Length: {adminPassword?.Length ?? 0} characters");

            if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            {
                Console.WriteLine("❌ ERROR: Admin email or password is not configured in appsettings.json!");
                return;
            }

            // Create role if not exists
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
                if (roleResult.Succeeded)
                {
                    Console.WriteLine("✅ Admin role created successfully");
                }
                else
                {
                    Console.WriteLine("❌ Failed to create Admin role:");
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine($"   - {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Admin role already exists");
            }

            // Check if admin user exists
            var user = await userManager.FindByEmailAsync(adminEmail);

            if (user == null)
            {
                Console.WriteLine("👤 Creating admin user...");

                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Admin user created and assigned to Admin role successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to create admin user:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"   - {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists");

                // Check if user is in Admin role
                var isInRole = await userManager.IsInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    Console.WriteLine("Added existing user to Admin role");
                }
                else
                {
                    Console.WriteLine("User is already in Admin role");
                }
            }
        }
    }
}
