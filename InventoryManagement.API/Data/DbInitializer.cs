using InventoryManagement.API.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.API.Data;

public static class DbInitializer
{
    public static async Task SeedAdminUser(
        UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "admin@test.com";

        var admin =
            await userManager.FindByEmailAsync(
                adminEmail);

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };

            await userManager.CreateAsync(
                admin,
                "Admin@123"
            );

            await userManager.AddToRoleAsync(
                admin,
                "Admin"
            );
        }
    }
}