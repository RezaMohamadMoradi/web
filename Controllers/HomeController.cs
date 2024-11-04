using _1.Models;
using Dto.Payment;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics;
using System.Security.Claims;
using YourNamespace;
using ZarinPal.Class;


namespace _1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly YourDbContext _context;
        private readonly UserManager<User> _userManager;  // تغییر IdentityUser به User

        public HomeController(ILogger<HomeController> logger, YourDbContext context, UserManager<User> userManager)
        {
            _context
     = context;
            _logger = logger;
            _userManager = userManager;

        }

        public IActionResult Index()
        {

            // نمونه‌ای از لیست دسته‌بندی‌ها از دیتابیس

            var products = _context.products.ToList();
            var categories = _context.Categuries.ToList();

            // بررسی داده‌های بازیابی شده
            if (products == null || categories == null)
            {
                // اگر داده‌ها نال هستند، پیامی لاگ کنید
                Console.WriteLine("Products or Categories are null");
            }

            // ایجاد ViewModel و پر کردن آن با داده‌ها
            var viewModel = new catpor
            {
                productts = products,
                Categuries = categories
            };

            // بررسی ViewModel قبل از ارسال به ویو
            if (viewModel.productts == null || viewModel.Categuries == null)
            {
                // اگر ViewModel نال است، پیامی لاگ کنید
                Console.WriteLine("ViewModel contains null data");
            }

            // ارسال ViewModel به View
            return View(viewModel);

        }
        public IActionResult detail(int id)
        {
            
            // پیدا کردن محصول بر اساس شناسه
            var p = _context.products.Include(i=>i.item)
                             // شامل موجودیت مرتبط item
                            .SingleOrDefault(p => p.id == id);  // پیدا کردن محصول با id مشخص

            // پیدا کردن دسته‌بندی مرتبط با محصول
            var c = _context.Categuries
                            .Where(cat => cat.id == p.categuryid)  // پیدا کردن دسته‌بندی مرتبط با محصول
                            .ToList();

            // ساخت ViewModel با محصول و دسته‌بندی‌ها
            var v = new detailsViewModel()
            {
                categures = c,  // لیست دسته‌بندی‌ها
                product = p     // محصول پیدا شده
            };

            return View(v);  // ارسال ViewModel به View
        }
        public IActionResult addcart(int id)
        {
            // یافتن محصولی که باید به سبد خرید اضافه شود
            var p = _context.products.Include(p => p.item).SingleOrDefault(p => p.itemid == id);
            if (p != null)
            {
                // یافتن سبد خرید کاربر از دیتابیس
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // فرض کنیم User.Identity کاربر را برگرداند
               var cart = _context.carts
                   .Include(c => c.Cartitems)
                   .ThenInclude(ci => ci.item)
                   .ThenInclude(i => i.product)  // بارگذاری product
                   .FirstOrDefault(c => c.userid == userId && !c.isfinaly);


                // اگر سبد خرید کاربر وجود نداشت، یک سبد خرید جدید بسازید
                if (cart == null)
                {
                    cart = new cart { userid = userId, createfactor = DateTime.Now, isfinaly = false };
                    _context.carts.Add(cart);
                }

                // اضافه کردن آیتم به سبد خرید
                var cartitem = cart.Cartitems.FirstOrDefault(ci => ci.item.id == p.item.id);
                if (cartitem != null)
                {
                    cartitem.quantity++;
                }
                else
                {
                    cartitem = new cartitem { item = p.item, quantity = 1 };
                    cart.Cartitems.Add(cartitem);
                }

                // ذخیره تغییرات در دیتابیس
                _context.SaveChanges();

                return RedirectToAction("showcart");
            }
            else
            {
                return NotFound();
            }
        }
       
        public IActionResult showcart()
        {
            // گرفتن کاربر فعلی
            var userId = _userManager.GetUserId(User);

            // گرفتن سبد خرید کاربر از دیتابیس
            var userCart = _context.carts
                         .Include(c => c.Cartitems) // بارگذاری Cartitems
                         .ThenInclude(ci => ci.item)
                          .ThenInclude(i => i.product)// بارگذاری item های مرتبط
                         .FirstOrDefault(c => c.userid == userId && !c.isfinaly);
          

            if (userCart == null)
            {
                return View(new cartviewmodel
                {
                    Cartitems = new List<cartitem>(),
                    ordertotal = 0
                });
            }

            // ساختن ViewModel برای نمایش
            var cartvm = new cartviewmodel
            {
                Cartitems = userCart.Cartitems,
                productts = userCart.Cartitems.Select(ci => ci.item.product).ToList(), // محصولات مرتبط با cartitem ها

                ordertotal = userCart.Cartitems.Sum(c => c.getddecimalprice())
            };
          

            return View(cartvm);
        }

        public IActionResult deletecart(int id)
        {
            // گرفتن کاربر فعلی
            var userId = _userManager.GetUserId(User);

            // گرفتن سبد خرید کاربر
            var userCart = _context.carts.Include(c => c.Cartitems)
                                         .ThenInclude(ci => ci.item)
                                         .FirstOrDefault(c => c.userid == userId && !c.isfinaly);

            if (userCart != null)
            {
                // پیدا کردن آیتم در سبد خرید
                var cartItem = userCart.Cartitems.SingleOrDefault(ci => ci.item.id == id);

                if (cartItem != null)
                {
                    if (cartItem.quantity > 1)
                    {
                        // اگر تعداد بیشتر از 1 بود، یکی کم می‌کنیم
                        cartItem.quantity--;
                    }
                    else
                    {
                        // اگر تعداد 1 بود، آیتم را حذف می‌کنیم
                        userCart.Cartitems.Remove(cartItem);
                    }

                    // ذخیره تغییرات
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("showcart");
        }

        public IActionResult Privacy()
        {
            return View();
        }
       

        [Route(template:"s")] /*Route atribiot*/
        public IActionResult s()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<productt> liproduct(int id)
        {
            // کوئری برای دریافت محصولات مرتبط با یک دسته‌بندی خاص
            var p = _context.products
                            .Where(prod => prod.categuryid == id)  // فیلتر بر اساس دسته‌بندی
                            .ToList();  // تبدیل به لیست

            return p;

        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categuries.ToListAsync();
            return PartialView("listdropcat", categories);
        }
        [Authorize]
        public async Task<IActionResult> InitiatePayment()
        {
            //int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var nameIdentifierClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = _context.carts
                .Include(c => c.Cartitems)
                 .ThenInclude(ci => ci.item)
           .FirstOrDefault(c => c.userid == nameIdentifierClaim && !c.isfinaly);

            if (cart == null)
                return NotFound();

            decimal totalAmount = cart.Cartitems.Sum(ci => ci.item.price * ci.quantity);

            var payment = new Payment();

            // ساخت یک درخواست پرداخت
            var dtoRequest = new DtoRequest()
            {
                MerchantId = "YourMerchantId", // شناسه زرین پال
                Amount = (int)totalAmount, // مبلغ کل
                CallbackUrl = "http://localhost:1635/Home/OnlinePayment/" + cart.Id, // آدرس بازگشت
                Description = $"پرداخت فاکتور شماره {cart.Id}",
                Email = "YourEmail@example.com",
                Mobile = "YourPhoneNumber"
            };

            var res = await payment.Request(dtoRequest, Payment.Mode.sandbox); // استفاده از sandbox برای محیط تست

            if (res.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Authority);
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] == "OK" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];
                var cart = _context.carts.Include(c => c.Cartitems)
                    .FirstOrDefault(c => c.Id == id);

                if (cart == null)
                    return NotFound();

                decimal totalAmount = cart.Cartitems.Sum(ci => ci.item.price * ci.quantity);

                var payment = new Payment();
                var dtoVerification = new DtoVerification()
                {
                    MerchantId = "YourMerchantId", // شناسه زرین پال
                    Amount = (int)totalAmount,
                    Authority = authority
                };

                var res = await payment.Verification(dtoVerification, Payment.Mode.sandbox);

                if (res.Status == 100)
                {
                    cart.isfinaly = true;
                    _context.carts.Update(cart);
                    await _context.SaveChangesAsync();
                    ViewBag.RefId = res.RefId;
                    return View();
                }
            }

            return NotFound();
        }

    }
}
