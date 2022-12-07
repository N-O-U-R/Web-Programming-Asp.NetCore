using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    public class Login
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public int PassWord { get; set; }
    }
}
