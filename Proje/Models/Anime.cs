

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proje.Models
{

    public class Anime
    {
        
        [Key]
        public int animeId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a value.")]
        public string animeTitle { get; set; }
        [Display(Name ="Poster URL")]
        [Required(ErrorMessage = "Please enter a value.")]
        public string animePoster { get; set; }
        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name ="Rating")]
        [Range(0.1, 10, ErrorMessage = "Please enter a correct value.")]
        public double animeRating { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Number of episodes")]
        [Range(1, 2000, ErrorMessage = "Please enter a correct value.")]
        public int animeEpisodes { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Start Year")]
        [rangeYear(1900, ErrorMessage = "Please enter a correct value.")]
        public int animeStartYear { get; set; }

        [Display(Name = "End Year")]
        [Required(ErrorMessage = "Please enter a value.")]
        [rangeYear(1900, ErrorMessage = "Please enter a correct value.")]
        public  int animeEndYear { get; set; }

        [Required(ErrorMessage = "Please enter a value.")]
        [Display(Name = "Plot Summery")]
        public string animeStory { get; set; }


        [Display(Name = "Categories")]
        public string animeCategories { get; set; }


        [NotMapped]
        public IEnumerable<Category>? categoryCollection { get; set; }
        
        [NotMapped]
     
        public string[]? animeCategoryArray { get; set; }

        public ICollection<AnimeUser>? users { get; set; }
    }
}
