using Proje.Models;

namespace Proje.Data
{
    public class AnimeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Anime> Animes{ get; set; }
    }
}
