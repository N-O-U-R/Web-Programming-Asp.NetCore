using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    public class AnimeUser
    {
        public int animeId { get; set; }
        public Anime? anime { get; set; }

        public string userId { get; set; }
        public AppUser?  user { get; set; }


        [Required(ErrorMessage ="Please select an option")]
        [Display(Name ="Status")]
        public string watchStatus { get;set; }


        [Required(ErrorMessage = "Please select a score")]
        [Display(Name = "Your Score")]
        public int userRating { get; set; }
    }
}
