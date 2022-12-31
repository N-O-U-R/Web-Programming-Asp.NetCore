using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proje.Models
{
    public class TvShowUser
    {
        public int showId { get; set; }
        public TvShow? tvShow { get; set; }

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
