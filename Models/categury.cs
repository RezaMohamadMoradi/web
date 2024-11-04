using System.ComponentModel.DataAnnotations;
using YourNamespace;

namespace _1.Models
{
    public class categury
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
        public string description { get; set; }

        // لیستی از محصولات که به این دسته‌بندی مرتبط هستند
        public List<productt> Products { get; set; }
    }

}
