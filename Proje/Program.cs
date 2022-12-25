using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Proje.Models;
using Proje.ViewModel;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthentcation/Login");

//builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddDbContext<ShowContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//    {
//        options.Password.RequiredLength = 3;
//        options.Password.RequireUppercase = false;
//        options.Password.RequireLowercase = true;

//        options.Lockout.MaxFailedAccessAttempts = 5;
//        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
//        options.User.RequireUniqueEmail = true;
//    })
//    .AddEntityFrameworkStores<ShowContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;

        options.User.RequireUniqueEmail = true;
        
    })
    .AddEntityFrameworkStores<ShowContext>();


builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options => options.DefaultScheme=CookieAuthenticationDefaults.AuthenticationScheme);


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//});

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
Init.SeedUsersAndRolesAsync(app).Wait();
app.Run();
