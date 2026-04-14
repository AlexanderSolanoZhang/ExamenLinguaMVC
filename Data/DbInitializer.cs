using ExamenLinguaMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace ExamenLinguaMVC.Data
{
    public class DbInitializer
    {
        public static async Task InicializacionRolesyAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Administrador", "Usuario" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminEmail = "admin@correo.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Direccion = "Direccion Admin",
                    Telefono = "1234567890"
                };

                string adminPassword = "Admin@123";

                var createAdminUser = await userManager.CreateAsync(newAdminUser, adminPassword);

                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Administrador");
                }
            }
        }
    }
}