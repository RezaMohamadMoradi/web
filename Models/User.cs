using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace _1.Models
{
    public class User : IdentityUser
    {

        public string? name { get; set; }
        public bool admin { get; set; }

        public cart cart { get; set; }
    }
}
