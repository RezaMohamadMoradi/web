using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1.Models
{
    public class deler
    {
        [Key]
        public int id { get; set; }
        public DateTime createfactor { get; set; }
        public bool isfinaly { get; set; }

        // Foreign Key
        public string userid { get; set; }

        // Navigation property
        [ForeignKey("userid")]
        public User User { get; set; }

        public List<productt> productts { get; set;}
    }

}
