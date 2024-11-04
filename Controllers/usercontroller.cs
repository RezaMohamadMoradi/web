using _1.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Runtime.InteropServices;
using System.Security.Claims;
using YourNamespace;


namespace _1.Controllers
{
    public class usercontroller : Controller
    {
        private readonly UserManager<User> _userManager;  // تغییر IdentityUser به User
        private readonly SignInManager<User> _signInManager;
        private readonly YourDbContext _context;
        private readonly PasswordHasher<IdentityUser> _passwordHasher;



        public usercontroller(UserManager<User> userManager, SignInManager<User> signInManager, YourDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _passwordHasher = new PasswordHasher<IdentityUser>();

        }
        [HttpGet]
        public IActionResult register()
        {
            return View("Views/Home/viewregister.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> add(userviewmodel u)
        {
            var user = new User()
            {
                name = u.name,
                UserName=u.name,
                admin = u.admin,
            };
            var result = await _userManager.CreateAsync(user,u.password);
            return View("Views/Home/viewregister.cshtml");

        }
        public IActionResult viewlogin()
        {
            return View("Views/Home/viewlogin.cshtml");
        }

        //[HttpPost]
        //public async Task<IActionResult> Loginn(loginmodel l)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Views/Home/viewlogin.cshtml");

        //    }
        //    else
        //    {
        //        var user = _context.login(l.LoginName.ToLower(), l.Password);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("LoginName", "اطلاعات صحیح نیست");
        //            return View("Views/Home/viewlogin.cshtml");

        //        }
        //        else
        //        {
        //            با این قطعه کد ها هر چی از کابر که لاگین شده رو میتونیم پس بگیریم
        //            var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        //        new Claim(ClaimTypes.Name,user.name)
        //    };
        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);
        //            var properties = new AuthenticationProperties
        //            {
        //                IsPersistent = true
        //            };
        //            await HttpContext.SignInAsync(principal, properties);
        //            return Redirect("/");
        //        }
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Loginn(loginmodel model)
        {
            if (ModelState.IsValid)
            {
                // ابتدا کاربر را از دیتابیس بر اساس LoginName پیدا می‌کنیم
                var user = await _userManager.FindByNameAsync(model.LoginName);
                if (user != null)
                {
                    // بررسی اعتبار رمز عبور
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        // ایجاد لیستی از Claims
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName), // نام کاربری
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // اضافه کردن NameIdentifier
                    new Claim("admin", user.admin.ToString())   // افزودن Claim مربوط به admin
                };

                        // ساختن Identity با Claims
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // ایجاد Principal
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        // SignIn کردن کاربر با Claims
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        // هدایت کاربر به صفحه اصلی
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View("/Views/Home/viewlogin.cshtml", model);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult AccessDenied()
        {
            return View("/Views/Home/viewlogin.cshtml");
        }
        

    }
}