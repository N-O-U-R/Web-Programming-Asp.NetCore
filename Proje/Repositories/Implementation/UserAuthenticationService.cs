using Microsoft.AspNetCore.Identity;
using Proje.Models;
using Proje.Models.Domain;
using Proje.Repositories.Abstract;
using System.Security.Claims;

namespace Proje.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationUser> roleManager;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager ,RoleManager<ApplicationUser> roleManager)

        {
            this.signInManager = signInManager; 
            this.userManager = userManager; 
            this.roleManager = roleManager; 
        }
        public int MyProperty { get; set; }

        public async Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username)
        {
            var status = new Status();

            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                status.Message = "User does not exist";
                status.StatusCode = 0;
                return status;
            }
            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                status.Message = "Password has updated successfully";
                status.StatusCode = 1;
            }
            else
            {
                status.Message = "Some error occcured";
                status.StatusCode = 0;
            }
            return status;

        }

        public async Task<Status> LoginAsync(Login model)
        {
           var status = new Status();   
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "User could not found  ";
                return status; 
            }
            //match the password 
             
            if (!await userManager.CheckPasswordAsync(user,  model.PassWord))
            {
                status.StatusCode = 0;
                status.Message = "Password Invalid ";
                return status;  
            }

            var signInResult = await signInManager.PasswordSignInAsync(user , model.PassWord , false , true);
            if (signInResult.Succeeded)
            {
                //roll managmet
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName)
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add( new Claim (ClaimTypes.Role , userRole)); 
                }
                //last check 
                status.StatusCode = 1;
                status.Message = "Log in went Succssesfully  ";
                return status;  

            }
            else if(!signInResult.IsLockedOut)
            {
                status.StatusCode = 0 ;
                status.Message = "User is Locked out ";
                return status;
            }

            else
            {
                status.StatusCode = 0 ;
                status.Message = "  Loginig in failed  ";
                return status;

            }


        }

        public async Task LogoutAsync()
        {
            await signInManager .SignOutAsync();    

        }

        public async Task<Status> RegisterAsync(Register model)
        {
           var status = new Status();   
           var userExists= await userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "user is already exists ";
                return status; 
            }
            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = model.UserName,
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = true ,  
            };
            var result = await userManager.CreateAsync(user, model.PassWord); 
            if(!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "user or password invalid ";
                return status; 
            }
            //role Management
            if (!await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.CreateAsync(user, model.Role);
            }
            if(await roleManager.RoleExistsAsync(model.Role))
            { await userManager.AddToRoleAsync(user, model.Role); }

            status.StatusCode = 1;
            status.Message = "User has Registered Successfully  ";
            return status ; 
                

          
        }
    }
}
