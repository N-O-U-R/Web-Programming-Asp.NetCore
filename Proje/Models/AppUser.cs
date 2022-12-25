using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Proje.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        [Display(Name="Full Name")]
        public string Name { get; set; }
    }
}
