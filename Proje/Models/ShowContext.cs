using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Proje.Models
{
    public class ShowContext:DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<TvShow> Shows { get; set; }
        public DbSet<Anime> Animes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Shows; Trusted_Connection=True;");
        }
    }
}
