
using Proje.Data;
using System.ComponentModel.DataAnnotations;

namespace Proje.Models
{
    public class Movie
    {
        [Key]
        public int movieId { get; set; }
        [Display(Name = "Title")]
        public string movieTitle { get; set; }
        [Display(Name = "Release Year")]
        public int movieYear { get; set; }

        public string moviePoster { get; set; }
        [Display(Name = "Rating")]
        public int movieRating { get; set; }
        [Display(Name = "Plot Summery")]
        public string movieStory { get; set; }
        [Display(Name = "Running Time")]
        public int movieRunningTime { get; set; }
        [Display(Name = "Categories")]
        public ICollection<MovieCategory> movieCategory { get; set; }

    }
}
