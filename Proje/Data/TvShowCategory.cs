using Proje.Models;

namespace Proje.Data
{
    public class TvShowCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TvShow> TvShows { get; set; }

    }
}
