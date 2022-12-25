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


                //string appUserEmail = "user@sakarya.edu.tr";

                //var appUser = await userManager.FindByEmailAsync(appUserEmail);
                //if (appUser == null)
                //{
                //    var newAppUser = new AppUser()
                //    {
                //        Name = "Application User",
                //        UserName = "app-user",
                //        Email = appUserEmail,
                //        EmailConfirmed = true
                //    };
                //    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                //    await userManager.AddToRoleAsync(newAppUser, userRoles.user);
                //}
            }

        }

        
    }
}
            
    

