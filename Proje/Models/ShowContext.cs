using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Proje.Models
{
    public class ShowContext:DbContext
    {
        public DbSet<Anime> animes { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<TvShow> tvShows { get; set; }

        public DbSet<Category> categories { get; set; }

  
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Shows; Trusted_Connection=True;");
        }
    }
}
