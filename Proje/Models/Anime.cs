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
        [Range(0, 10, ErrorMessage = "Please enter a correct value.")]
        public int animeRating { get; set; }
        [Display(Name = "Number of episodes")]
        [Range(0, 2000, ErrorMessage = "Please enter a correct value.")]
        public int animeEpisodes { get; set; }
        [Display(Name = "Start Year")]
        [Range(1950,2022, ErrorMessage = "Please enter a correct value.")]
        public int animeStartYear { get; set; }
        [Display(Name = "End Year")]    
        [Range(1950, 2023, ErrorMessage = "Please enter a correct value.")]
        public int animeEndYear { get; set; }
        [Display(Name = "Plot Summery")]

        public string animeStory { get; set; }
        [NotMapped]
        public ICollection<Category> categoryCollection { get; set; }
        [Display(Name = "Categories")]
        [NotMapped]
        public string[] animeCategories { get; set; }


    }
}
