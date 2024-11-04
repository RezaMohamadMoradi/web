using _1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using YourNamespace;


namespace _1.Controllers
{
    public class product : Controller
    {
        private readonly YourDbContext _context;
        public product(YourDbContext context)
        {
            _context = context;

        }
        [Route(template:"Group/{id}/{name}")]
        public IActionResult Index(int id, string name)
        {
            // تنظیم نام دسته‌بندی در ViewData
            ViewData["GroupName"] = name;

            // دریافت لیست محصولات مرتبط با دسته‌بندی خاص
            var p = _context.products
                            .Where(prod => prod.categuryid == id)  // فیلتر محصولات بر اساس دسته‌بندی
                            .ToList();  // تبدیل نتیجه به لیست

            return View(p);  // ارسال لیست محصولات به View
        }

    }
}
