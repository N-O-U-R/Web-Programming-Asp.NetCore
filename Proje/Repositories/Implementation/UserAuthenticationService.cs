﻿using Microsoft.AspNetCore.Identity;
using Proje.Models;
using Proje.Models.Domain;
using Proje.Repositories.Abstract;

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

        public Task<Status> LoginAsync(Login model)
        {
            throw new NotImplementedException();
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
                status.StatuCode = 0;
                status.Message = "user is already exists ";
                return status; 
            }
            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.UserName,
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = true ,  
            };
            var result = await userManager.CreateAsync(user, model.PassWord); 
            if(!result.Succeeded)
            {
                status.StatuCode = 0;
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

            status.StatuCode = 1;
            status.Message = "User has Registered Successfully  ";
            return status ; 
                

          
        }
    }
}
