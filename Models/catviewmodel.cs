namespace _1.Models
{
    public class cartviewmodel
    {
        public cartviewmodel()
        {
            Cartitems = new List<cartitem>();
        }
        public List<cartitem> Cartitems { get; set; }  
        public List<productt> productts { get; set; }
        public decimal ordertotal {  get; set; }
    }
}
