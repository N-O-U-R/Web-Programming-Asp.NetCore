﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proje.Models;

#nullable disable

namespace Proje.Migrations
{
    [DbContext(typeof(ShowContext))]
    [Migration("20230101132913_second2")]
    partial class second2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Proje.Models.Anime", b =>
                {
                    b.Property<int>("animeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("animeId"), 1L, 1);

                    b.Property<string>("animeCategories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("animeEndYear")
                        .HasColumnType("int");

                    b.Property<int>("animeEpisodes")
                        .HasColumnType("int");

                    b.Property<string>("animePoster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("animeRating")
                        .HasColumnType("float");

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

            modelBuilder.Entity("Proje.Models.AnimeUser", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("animeId")
                        .HasColumnType("int");

                    b.Property<int>("userRating")
                        .HasColumnType("int");

                    b.Property<string>("watchStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId", "animeId");

                    b.HasIndex("animeId");

                    b.ToTable("anime_Users");
                });

            modelBuilder.Entity("Proje.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Proje.Models.Category", b =>
                {
                    b.Property<int>("categoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("categoryId"), 1L, 1);

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("movieId"), 1L, 1);

                    b.Property<string>("movieCategories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("moviePoster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("movieRating")
                        .HasColumnType("float");

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

            modelBuilder.Entity("Proje.Models.MovieUser", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("movieId")
                        .HasColumnType("int");

                    b.Property<int>("userRating")
                        .HasColumnType("int");

                    b.Property<string>("watchStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId", "movieId");

                    b.HasIndex("movieId");

                    b.ToTable("movie_Users");
                });

            modelBuilder.Entity("Proje.Models.TvShow", b =>
                {
                    b.Property<int>("showId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("showId"), 1L, 1);

                    b.Property<string>("showCategories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("showEndYear")
                        .HasColumnType("int");

                    b.Property<int>("showEpisodes")
                        .HasColumnType("int");

                    b.Property<string>("showPoster")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("showRating")
                        .HasColumnType("float");

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

            modelBuilder.Entity("Proje.Models.TvShowUser", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("showId")
                        .HasColumnType("int");

                    b.Property<int>("userRating")
                        .HasColumnType("int");

                    b.Property<string>("watchStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId", "showId");

                    b.HasIndex("showId");

                    b.ToTable("tvShow_Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Proje.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Proje.Models.AnimeUser", b =>
                {
                    b.HasOne("Proje.Models.Anime", "anime")
                        .WithMany("users")
                        .HasForeignKey("animeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.AppUser", "user")
                        .WithMany("animes")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("anime");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Proje.Models.MovieUser", b =>
                {
                    b.HasOne("Proje.Models.Movie", "movie")
                        .WithMany("users")
                        .HasForeignKey("movieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.AppUser", "user")
                        .WithMany("movies")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("movie");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Proje.Models.TvShowUser", b =>
                {
                    b.HasOne("Proje.Models.TvShow", "tvShow")
                        .WithMany("users")
                        .HasForeignKey("showId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proje.Models.AppUser", "user")
                        .WithMany("tvShows")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tvShow");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Proje.Models.Anime", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("Proje.Models.AppUser", b =>
                {
                    b.Navigation("animes");

                    b.Navigation("movies");

                    b.Navigation("tvShows");
                });

            modelBuilder.Entity("Proje.Models.Movie", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("Proje.Models.TvShow", b =>
                {
                    b.Navigation("users");
                });
#pragma warning restore 612, 618
        }
    }
}
