using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class Movie
    {
        [Key]
        public int movieId { get; set; }
        [Display(Name = "Title")]
        public string movieTitle { get; set; }
        [Display(Name = "Release Year")]
        [Range(1900, 2023, ErrorMessage = "Please enter a correct value.")]
        public int movieYear { get; set; }

        public string moviePoster { get; set; }
        [Display(Name = "Rating")]
        [Range(0,10,ErrorMessage ="Please enter a correct value.")]
        public int movieRating { get; set; }
        [Display(Name = "Plot Summery")]
        public string movieStory { get; set; }
        [Display(Name = "Running Time")]
        [Range(0, 450, ErrorMessage = "Please enter a correct value.")]
        public int movieRunningTime { get; set; }
        [Display(Name = "Categories")]
        public string? movieCategories { get; set; }
        [NotMapped]
        public IEnumerable<Category>? categoryCollection { get; set; }
        [NotMapped]
        public string[]? movieCategoryArray { get; set; }

    }
}
