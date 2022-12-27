using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class Movie
    {
        [Key]
        public int movieId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a value.")]
        public string movieTitle { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Release Year")]
        [rangeYear(1900, ErrorMessage = "Please enter a correct value.")]
        public int movieYear { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name ="Poster URL")]
        public string moviePoster { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Rating")]
        [Range(0,10,ErrorMessage ="Please enter a correct value.")]
        public double movieRating { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Plot Summery")]
        public string movieStory { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Running Time")]
        [Range(0, 450, ErrorMessage = "Please enter a correct value.")]
        public int movieRunningTime { get; set; }


        [Display(Name = "Categories")]

        public string? movieCategories { get; set; }
        [NotMapped]
        public IEnumerable<Category>? categoryCollection { get; set; }
        [NotMapped]
        public string[]? movieCategoryArray { get; set; }

        //public ICollection<Movie_User>? movie_Users { get; set; }

    }
}
