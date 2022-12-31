using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class TvShow
    {
        [Key]
        public int showId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a value.")]
        public string showTitle { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name ="Poster URL")]
        public string showPoster { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Start Year")]
        [rangeYear(1900, ErrorMessage = "Please enter a correct value.")]
        public int showStartYear { get; set; }

        
        [Display(Name = "End Year (if it's still airing leave empty)")]
        [rangeYear(1900, ErrorMessage = "Please enter a correct value.")]
        public int? showEndYear{ get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Number of episodes")]
        [Range(1, 2000, ErrorMessage = "Please enter a correct value.")]
        public int showEpisodes { get; set; }


        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Rating")]
        [Range(0, 10, ErrorMessage = "Please enter a correct value.")]
        public double showRating { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Plot Summery")]
        public string showStory { get; set; }


        [Display(Name = "Categories")]
 
        public string? showCategories { get; set; }
        [NotMapped]
        public IEnumerable<Category>? categoryCollection { get; set; }
        [NotMapped]
        public string[]? showCategoryArray { get; set; }


        public ICollection<TvShowUser>? users { get; set; }
    }
}
