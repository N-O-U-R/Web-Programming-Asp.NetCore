using Proje.Data;
using System.ComponentModel.DataAnnotations;

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
        public int showStartYear { get; set; }
        [Display(Name = "End Year")]
        public int showEndYear{ get; set; }
        [Display(Name = "Number of episodes")]
        public int showEpisodes { get; set; }

        [Display(Name = "Rating")]
        public int showRating { get; set; }
        [Display(Name = "Plot Summery")]
        public string showStory { get; set; }
        [Display(Name = "Categories")]
        public Category showCategory { get; set; }

    }
}
