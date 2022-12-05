using Proje.Models;

namespace Proje.Data
{
    public class MovieCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
