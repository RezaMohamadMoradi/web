using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _1.Models; // فضای نامی که مدل‌های شما در آن تعریف شده‌اند
using System.IO;
using System.Threading.Tasks;


using YourNamespace;

namespace _1.Pages.Admin
{
    public class EditProductModel : PageModel
    {
        private readonly YourDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditProductModel(YourDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public productt Product { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.products.Include(p => p.item).FirstOrDefaultAsync(p => p.id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var productToUpdate = await _context.products.Include(p => p.item).FirstOrDefaultAsync(p => p.id == id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Name = Product.Name;
            productToUpdate.Description = Product.Description;
            productToUpdate.item.price = Product.item.price;

            if (Image != null && Image.Length > 0)
            {
                var fileName = $"{productToUpdate.id}_{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
                var filePath = Path.Combine(_environment.WebRootPath, "Img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(productToUpdate.img) && productToUpdate.img != "/Img/default.jpg")
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, productToUpdate.img.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                productToUpdate.img = $"/Img/{fileName}";
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin");
        }
    }
}
