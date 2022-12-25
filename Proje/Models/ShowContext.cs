﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Proje.Models
{
    public class ShowContext:IdentityDbContext<AppUser>
    {
        public ShowContext(DbContextOptions<ShowContext> options):base(options)
        {
        }
        
        public DbSet<Anime> animes { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<TvShow> tvShows { get; set; }

        public DbSet<Category> categories { get; set; }

        //public DbSet<Anime_User> anime_Users { get; set; }
        //public DbSet<Movie_User> movie_Users { get; set; }
        //public DbSet<tvShow_User> tvShow_Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Anime_User>()
        //        .HasOne(a => a.anime)
        //        .WithMany(au => au.anime_Users)
        //        .HasForeignKey(ai => ai.animeId);

        //    modelBuilder.Entity<Anime_User>()
        //       .HasOne(a => a.user)
        //       .WithMany(au => au.animes)
        //       .HasForeignKey(u => u.userId);

        //    modelBuilder.Entity<Movie_User>()
        //        .HasOne(a => a.movie)
        //        .WithMany(au => au.movie_Users)
        //        .HasForeignKey(ai => ai.movieId);

        //    modelBuilder.Entity<Movie_User>()
        //       .HasOne(a => a.user)
        //       .WithMany(au => au.movies)
        //       .HasForeignKey(u => u.userId);

        //    modelBuilder.Entity<tvShow_User>()
        //        .HasOne(a => a.tvShow)
        //        .WithMany(au => au.tvShow_Users)
        //        .HasForeignKey(ai => ai.tvShowId);

        //    modelBuilder.Entity<tvShow_User>()
        //       .HasOne(a => a.user)
        //       .WithMany(au => au.tvShows)
        //       .HasForeignKey(u => u.userId);

        //}
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Shows; Trusted_Connection=True;");
        //}
    }
}
