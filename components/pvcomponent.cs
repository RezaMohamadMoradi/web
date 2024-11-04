using _1.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

using YourNamespace;

namespace _1.components

{

    public class pvcomponent : ViewComponent
    {
        public pvcomponent(YourDbContext context)
        {
            _context = context;

        }
        private readonly YourDbContext _context;

        public IViewComponentResult Invoke()
        {
            // View component logic here
            return View("~/Views/component/View.cshtml", _context.Categuries);
        }
        public IViewComponentResult Invoke2()
        {
            // View component logic here
            return View("~/Views/Shared/listdropcat.cshtml", _context.Categuries);
        }
    }


}

