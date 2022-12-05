using Proje.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{
    public class Anime
    {
        [Key]
        public int animeId { get; set; }
        [Display(Name = "Title")]

        public string animeTitle { get; set; }
        public string animePoster { get; set; }

        [Display(Name ="Rating")]
        public int animeRating { get; set; }
        [Display(Name = "Number of episodes")]

        public int animeEpisodes { get; set; }
        [Display(Name = "Start Year")]

        public int animeStartYear { get; set; }
        [Display(Name = "End Year")]

        public int animeEndYear { get; set; }
        [Display(Name = "Plot Summery")]

        public string animeStory { get; set; }
        [Display(Name = "Categories")]
        public Category animeCategory { get; set; }

    }
}
