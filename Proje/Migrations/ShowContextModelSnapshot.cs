﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proje.Models;

#nullable disable

namespace Proje.Migrations
{
    [DbContext(typeof(ShowContext))]
    partial class ShowContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Proje.Models.Anime", b =>
                {
                    b.Property<int>("animeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("animeId"));

                    b.Property<int>("animeEndYear")
                        .HasColumnType("int");

                    b.Property<int>("animeEpisodes")
                        .HasColumnType("int");

                    b.Property<string>("animePoster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("animeRating")
                        .HasColumnType("int");

                    b.Property<int>("animeStartYear")
                        .HasColumnType("int");

                    b.Property<string>("animeStory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("animeTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("animeId");

                    b.ToTable("animes");
                });

            modelBuilder.Entity("Proje.Models.Category", b =>
                {
                    b.Property<int>("categoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("categoryId"));

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("categoryId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Proje.Models.Movie", b =>
                {
                    b.Property<int>("movieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("movieId"));

                    b.Property<string>("moviePoster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("movieRating")
                        .HasColumnType("int");

                    b.Property<int>("movieRunningTime")
                        .HasColumnType("int");

                    b.Property<string>("movieStory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("movieTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("movieYear")
                        .HasColumnType("int");

                    b.HasKey("movieId");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("Proje.Models.TvShow", b =>
                {
                    b.Property<int>("showId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("showId"));

                    b.Property<int>("showEndYear")
                        .HasColumnType("int");

                    b.Property<int>("showEpisodes")
                        .HasColumnType("int");

                    b.Property<string>("showPoster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("showRating")
                        .HasColumnType("int");

                    b.Property<int>("showStartYear")
                        .HasColumnType("int");

                    b.Property<string>("showStory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("showTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("showId");

                    b.ToTable("tvShows");
                });
#pragma warning restore 612, 618
        }
    }
}
