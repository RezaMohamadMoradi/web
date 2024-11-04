using System.ComponentModel.DataAnnotations;

namespace _1.Models
{
    public class item
    {
        
        [Key]  public int id { get; set; }
        

        public int price { get; set; }

        
        public int quantity { get; set; }

        public productt product { get; set; }
    }
}
