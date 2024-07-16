using FootScout.WebAPI.DbContext;
using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FootScout.WebAPI.DbHandler
{
    public static class AppSeeder
    {
        public static async Task Seed(IServiceProvider services)
        {
            await SeedRoles(services);
            await SeedAdminUser(services);
        }

        private static async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Role.Admin));
            await roleManager.CreateAsync(new IdentityRole(Role.User));
        }

        private static async Task SeedAdminUser(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var adminUser = await context.Users.FirstOrDefaultAsync(user => user.Email == "admin@admin.com");

            if (adminUser is null)
            {
                adminUser = new User { Email = "admin@admin.com" };
                await userManager.CreateAsync(adminUser, "Admin!1");
                await userManager.AddToRoleAsync(adminUser, Role.Admin);
            }
        }
    }
}