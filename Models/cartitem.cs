using YourNamespace;

namespace _1.Models
{
    public class cartitem
    {
        public int id { get; set; }

        public item item { get; set; }
        public cart cart { get; set; }
        public int cartid { get; set; }

        public int quantity { get; set; }
        public decimal getddecimalprice()
        {
            return item.price * quantity;
        }

    }
}
