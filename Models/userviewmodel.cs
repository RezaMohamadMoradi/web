using System.ComponentModel.DataAnnotations;

namespace _1.Models
{
    public class userviewmodel

    {
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [MaxLength(100)]
        [Display(Name = "نام")]

        public string name { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]

        public string password { get; set; }
        [MaxLength(100)]
        [DataType(DataType.Password)]
        [Compare("password")]
        [Display(Name="تکرار رمز عبور")]
        public string? repassword { get; set; }
        public bool admin { get; set; }
    }
}
