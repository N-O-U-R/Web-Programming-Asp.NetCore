using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class TvShow
    {
        [Key]
        public int showId { get; set; }
        [Display(Name = "Title")]
        public string showTitle { get; set; }
        public string showPoster { get; set; }
        [Display(Name = "Start Year")]
        [Range(1900, 2022, ErrorMessage = "Please enter a correct value.")]
        public int showStartYear { get; set; }
        [Display(Name = "End Year")]
        [Range(1900, 2023, ErrorMessage = "Please enter a correct value.")]
        public int showEndYear{ get; set; }
        [Display(Name = "Number of episodes")]
        [Range(1, 2000, ErrorMessage = "Please enter a correct value.")]
        public int showEpisodes { get; set; }

        [Display(Name = "Rating")]
        [Range(0, 10, ErrorMessage = "Please enter a correct value.")]
        public int showRating { get; set; }
        [Display(Name = "Plot Summery")]
        public string showStory { get; set; }
        [Display(Name = "Categories")]
        public string? showCategories { get; set; }
        [NotMapped]
        public IEnumerable<Category>? categoryCollection { get; set; }
        [NotMapped]
        public string[]? showCategoryArray { get; set; }

    }
}
