using _1.Models;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YourNamespace;

var builder = WebApplication.CreateBuilder(args);

// تنظیمات مربوط به DbContext
builder.Services.AddDbContext<YourDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// افزودن سرویس‌های Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
  
    options.Password.RequiredLength = 4;
    // سایر تنظیمات مربوط به رمز عبور
})
.AddEntityFrameworkStores<YourDbContext>()
.AddDefaultTokenProviders();

// پیکربندی کوکی برای Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "cookie";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/user/viewlogin";
    options.AccessDeniedPath = "/user/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});

// افزودن احراز هویت کوکی
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/user/viewlogin";
        options.LogoutPath = "/user/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(4);
    });



// افزودن MVC و Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware برای هندل کردن خطاها و استفاده از HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


// مسیریابی پیش‌فرض
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
