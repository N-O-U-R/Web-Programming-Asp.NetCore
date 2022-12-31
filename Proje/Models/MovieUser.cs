using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proje.Models
{
    public class MovieUser
    {
        public int movieId { get; set; }
        public Movie? movie { get; set; }

        public string userId { get; set; }
        public AppUser? user { get; set; }


        [Required(ErrorMessage = "Please select an option")]
        [Display(Name = "Status")]
        public string watchStatus { get; set; }


        [Required(ErrorMessage = "Please select a score")]
        [Display(Name = "Your Score")]
        public int userRating { get; set; }
    }
}
