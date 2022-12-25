using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;
namespace Proje.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email Address is Invalid")]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Invalid")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
