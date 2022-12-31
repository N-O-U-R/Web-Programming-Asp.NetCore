using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Proje.Models;
using Proje.ViewModel;
using System;

namespace Proje.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userM;
        private readonly SignInManager<AppUser> _sinInM;
        private readonly ShowContext _context;


        public AccountController(UserManager<AppUser> userM, SignInManager<AppUser> sinInM, ShowContext context)
        {
            _userM = userM;
            _sinInM = sinInM;
            _context = context;
        }
        //public async Task<IActionResult> Users()
        //{
        //    var users = await _context.Users.ToListAsync();
        //    return View(users);
        //}
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)

        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userM.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var passwordCheck = await _userM.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _sinInM.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email or Password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                }
                
                return View(loginVM);
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
            }


            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userM.FindByEmailAsync(registerVM.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new AppUser
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.Email
            };

            var newUserResponse = await _userM.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                TempData["success"] = "Registration is Successful";
                await _userM.AddToRoleAsync(newUser, userRoles.user);

                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _sinInM.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
        //public IActionResult SetLanguage(string culture, string returnUrl)
        //{
        //    Response.Cookies.Append(
        //        CookieRequestCultureProvider.DefaultCookieName,
        //        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        //        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        //        );
        //    return LocalRedirect(returnUrl);
        //}


    }
}
