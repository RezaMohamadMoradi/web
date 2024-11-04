using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourNamespace;
using _1.Models;

namespace _1.Pages_Users
{

    public class IndexModel : PageModel
    {
        private readonly YourNamespace.YourDbContext _context;

        public IndexModel(YourNamespace.YourDbContext context)
        {
            _context = context;
        }

        public IList<User> Users { get;set; } = default!;

        //public IActionResult OnGet()
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToPage("/user/viewlogin");
        //    }

        //    var adminClaim = User.FindFirst("admin");
        //    if (adminClaim == null || adminClaim.Value != "true")
        //    {
        //        return RedirectToPage("/user/AccessDenied");
        //    }

        //    // „Õ Ê«? ’›ÕÂ
        //    return Page();
        //}
        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();
        }
    }
}
