using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Proje.Models;

namespace Proje.ViewModel
{
    public class Init
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(userRoles.admin))
                    await roleManager.CreateAsync(new IdentityRole(userRoles.admin));
                if (!await roleManager.RoleExistsAsync(userRoles.user))
                    await roleManager.CreateAsync(new IdentityRole(userRoles.user));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "b201210566@sakarya.edu.tr";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Name = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "sau");
                    await userManager.AddToRoleAsync(newAdminUser, userRoles.admin);
                }


                adminUserEmail = "b201210580@sakarya.edu.tr";

                var adminUser1 = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser1 == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Name = "Admin User",
                        UserName = "admin-user2",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "sau");
                    await userManager.AddToRoleAsync(newAdminUser, userRoles.admin);
                }
            }

        }

        
    }
}
            
    

