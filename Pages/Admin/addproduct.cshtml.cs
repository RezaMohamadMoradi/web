using _1.Controllers;
using _1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using YourNamespace;

namespace _1.Pages.Admin
{
    public class addproductModel : PageModel
    {
        private readonly YourDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public addproductModel(YourDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [BindProperty]
        public addpmodel product { get; set; }
        public List<categury> Categuries { get; set; }
        public void OnGet()
        {
            Categuries = _context.Categuries.ToList(); // لیست دسته‌بندی‌ها را از دیتابیس می‌گیریم

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // آیتم را ایجاد کنید
            var newItem = new item
            {
                price = product.price,
                quantity = product.quantity
            };

            _context.items.Add(newItem);
            await _context.SaveChangesAsync();

            // ذخیره تصویر محصول
            string fileName = null;
            if (product.img?.Length > 0)
            {
                fileName = $"{newItem.id}_{Guid.NewGuid()}{Path.GetExtension(product.img.FileName)}";
                var filePath = Path.Combine(_environment.WebRootPath, "Img", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.img.CopyToAsync(stream);
                }
            }
            else
            {
                fileName = "default.jpg";
            }

            // گرفتن دسته‌بندی انتخاب‌شده
            int selectedCategoryId = product.categuryid; // مقدار دسته‌بندی از فرم

            // ذخیره محصول در دیتابیس
            var productToSave = new productt
            {
                Name = product.Name,
                Description = product.Description,
                itemid = newItem.id,
                categuryid = selectedCategoryId, // استفاده از دسته‌بندی انتخاب‌شده
                img = $"/Img/{fileName}"
            };

            _context.products.Add(productToSave);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin");
        }



    }
}
