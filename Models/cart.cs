using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using YourNamespace;

namespace _1.Models
{
    public class cart
    {

        public int Id { get; set; }

        public List<cartitem> Cartitems { get; set; } = new List<cartitem>(); // تعریف به عنوان لیست یا ICollection

        public DateTime createfactor { get; set; }
        public bool isfinaly { get; set; }

        // Foreign Key
        public string userid { get; set; }

        // Navigation property
        [ForeignKey("userid")]
        public User User { get; set; }

        public List<productt> productts { get; set; }
        public void additem(cartitem item)
        {
            if (Cartitems.Exists(w => w.item.id == item.item.id))
            {
                Cartitems.Find(r => r.item.id == item.item.id)
                .quantity++;
            }
            else
            {
                Cartitems.Add(item);
            }
        }
        public void removeitem(int id)
        {
            var item = Cartitems.SingleOrDefault(a => a.item.id == id);

            if (item.quantity <= 1)
            {
                Cartitems.Remove(item);
            }
            else
            {
                item.quantity -= 1;
            }
        }

        
        }
}
