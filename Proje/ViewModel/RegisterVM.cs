using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proje.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Address is Invalid")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Invalid")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name="Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        public string? Role { get; set; }
    }
}
