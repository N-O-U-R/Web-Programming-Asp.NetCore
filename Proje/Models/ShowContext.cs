using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Proje.Models
{
    public class ShowContext : IdentityDbContext<AppUser>
    {
        public ShowContext(DbContextOptions<ShowContext> options) : base(options)
        {
        }

        public DbSet<Anime> animes { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<TvShow> tvShows { get; set; }

        public DbSet<Category> categories { get; set; }

        public DbSet<AnimeUser> anime_Users { get; set; }
        public DbSet<MovieUser> movie_Users { get; set; }
        public DbSet<TvShowUser> tvShow_Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<AnimeUser>()
                .HasKey(au => new { au.userId, au.animeId });


            modelBuilder.Entity<AnimeUser>()
                .HasOne(a => a.user)
                .WithMany(u => u.animes)
                .HasForeignKey(a => a.userId);

            modelBuilder.Entity<AnimeUser>()
               .HasOne(a => a.anime)
               .WithMany(au => au.users)
               .HasForeignKey(u => u.animeId);

            modelBuilder.Entity<MovieUser>()
                .HasKey(au => new { au.userId, au.movieId });


            modelBuilder.Entity<MovieUser>()
                .HasOne(a => a.movie)
                .WithMany(au => au.users)
                .HasForeignKey(ai => ai.movieId);

            modelBuilder.Entity<MovieUser>()
               .HasOne(a => a.user)
               .WithMany(au => au.movies)
               .HasForeignKey(u => u.userId);

            modelBuilder.Entity<TvShowUser>()
                .HasKey(au => new { au.userId, au.showId });

            modelBuilder.Entity<TvShowUser>()
                .HasOne(a => a.tvShow)
                .WithMany(au => au.users)
                .HasForeignKey(ai => ai.showId);

            modelBuilder.Entity<TvShowUser>()
               .HasOne(a => a.user)
               .WithMany(au => au.tvShows)
               .HasForeignKey(u => u.userId);


            base.OnModelCreating(modelBuilder);

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Shows; Trusted_Connection=True;");
        //}

    }
}
