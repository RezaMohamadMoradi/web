namespace _1.Models
{
    public class addpmodel
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public IFormFile img { get; set; }
        public int price { get; set; }

        public int quantity { get; set; }
        public int categuryid { get;  set; }
    }
}
