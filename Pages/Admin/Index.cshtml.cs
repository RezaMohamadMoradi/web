using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;
using _1.Models;
using YourNamespace;
using Microsoft.EntityFrameworkCore; // فضای نام مدل‌های شما

namespace _1.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly YourDbContext _context;

        public IndexModel(YourDbContext context)
        {
            _context = context;
        }

        public IPagedList<productt> Products { get; set; }

        public IActionResult OnGetAsync(int? page)
        {
            var pageNumber = page ?? 1; // اگر صفحه مشخص نشده باشد، پیش‌فرض صفحه اول است
            var pageSize = 10; // تعداد محصولات در هر صفحه

            Products =  _context.products.Include(p => p.item)
                               .OrderBy(p => p.id)
                               .ToPagedList(pageNumber, pageSize);

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // حذف آیتم مرتبط با محصول
            var item = await _context.items.FindAsync(product.itemid);
            if (item != null)
            {
                _context.items.Remove(item);
            }

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
        public void OnPost() { }
    }
}
    




