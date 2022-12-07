using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    public class Register
    {
        [Required]

        public String FullName { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]

        public String UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*()+=-]).{6,}$",ErrorMessage = "the password Length must be () 1 UpperLetter, one LowerLetter , onr spicial char and one digit")]
        public String PassWord { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("PassWord")]
        public String ConfirmPassWord { get; set; }
        [Required]
        public String Role { get; set; }
        

    }
}
