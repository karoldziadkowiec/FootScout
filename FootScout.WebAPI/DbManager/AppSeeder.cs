using FootScout.WebAPI.Entities;
using FootScout.WebAPI.Models.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FootScout.WebAPI.DbManager
{
    public static class AppSeeder
    {
        public static async Task Seed(IServiceProvider services)
        {
            using (var dbContext = services.GetRequiredService<AppDbContext>())
            {
                await SeedRoles(services);
                await SeedAdminRole(services);
                await SeedAdvertisementStatuses(services, dbContext);
            }
        }

        private static async Task SeedRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new List<string> { Role.Admin, Role.User };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private static async Task SeedAdminRole(IServiceProvider services)
        {
            string adminEmail = "admin@admin.com";
            string adminPassword = "Admin1!";

            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "000000000",
                    Location = "Admin",
                    CreationDate = DateTime.Now,
                };
                await userManager.CreateAsync(admin, adminPassword);
                await userManager.AddToRoleAsync(admin, Role.Admin);
            }
        }

        private static async Task SeedAdvertisementStatuses(IServiceProvider services, AppDbContext dbContext)
        {
            var statuses = new List<string> { Status.Accepted, Status.During, Status.Rejected };

            foreach (var status in statuses)
            {
                if (!await dbContext.AdvertisementStatuses.AnyAsync(s => s.StatusName == status))
                {
                    AdvertisementStatus newStatus = new AdvertisementStatus
                    {
                        StatusName = status,
                    };
                    dbContext.AdvertisementStatuses.Add(newStatus);
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}