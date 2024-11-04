using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace _1.Models
{
    public class productt
    {
        [Key]
        public int id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string img { get; set; }

        public int itemid { get; set; }

        // ارتباط با دسته‌بندی
        public int categuryid { get; set; }  // کلید خارجی به دسته‌بندی
        [ForeignKey("categuryid")]
        public categury Categury { get; set; } // ارتباط به موجودیت دسته‌بندی
        public List<cart> carts { get; set; }
        public item item { get; set; }

    }

}
