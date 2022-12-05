using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Migrations
{
    /// <inheritdoc />
    public partial class shows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                columns: table => new
                {
                    animeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    animePoster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    animeRating = table.Column<int>(type: "int", nullable: false),
                    animeEpisodes = table.Column<int>(type: "int", nullable: false),
                    animeStartYear = table.Column<int>(type: "int", nullable: false),
                    animeEndYear = table.Column<int>(type: "int", nullable: false),
                    animeStory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.animeId);
                });

            migrationBuilder.CreateTable(
                name: "MovieCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    movieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movieTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    movieYear = table.Column<int>(type: "int", nullable: false),
                    moviePoster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    movieRating = table.Column<int>(type: "int", nullable: false),
                    movieStory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    movieRunningTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.movieId);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    showId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    showTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    showPoster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    showStartYear = table.Column<int>(type: "int", nullable: false),
                    showEndYear = table.Column<int>(type: "int", nullable: false),
                    showEpisodes = table.Column<int>(type: "int", nullable: false),
                    showRating = table.Column<int>(type: "int", nullable: false),
                    showStory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.showId);
                });

            migrationBuilder.CreateTable(
                name: "TvShowCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeAnimeCategory",
                columns: table => new
                {
                    AnimesanimeId = table.Column<int>(type: "int", nullable: false),
                    animeCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeAnimeCategory", x => new { x.AnimesanimeId, x.animeCategoryId });
                    table.ForeignKey(
                        name: "FK_AnimeAnimeCategory_AnimeCategory_animeCategoryId",
                        column: x => x.animeCategoryId,
                        principalTable: "AnimeCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeAnimeCategory_Animes_AnimesanimeId",
                        column: x => x.AnimesanimeId,
                        principalTable: "Animes",
                        principalColumn: "animeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieMovieCategory",
                columns: table => new
                {
                    MoviesmovieId = table.Column<int>(type: "int", nullable: false),
                    movieCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieMovieCategory", x => new { x.MoviesmovieId, x.movieCategoryId });
                    table.ForeignKey(
                        name: "FK_MovieMovieCategory_MovieCategory_movieCategoryId",
                        column: x => x.movieCategoryId,
                        principalTable: "MovieCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieMovieCategory_Movies_MoviesmovieId",
                        column: x => x.MoviesmovieId,
                        principalTable: "Movies",
                        principalColumn: "movieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvShowTvShowCategory",
                columns: table => new
                {
                    TvShowsshowId = table.Column<int>(type: "int", nullable: false),
                    showCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvShowTvShowCategory", x => new { x.TvShowsshowId, x.showCategoryId });
                    table.ForeignKey(
                        name: "FK_TvShowTvShowCategory_Shows_TvShowsshowId",
                        column: x => x.TvShowsshowId,
                        principalTable: "Shows",
                        principalColumn: "showId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvShowTvShowCategory_TvShowCategory_showCategoryId",
                        column: x => x.showCategoryId,
                        principalTable: "TvShowCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeAnimeCategory_animeCategoryId",
                table: "AnimeAnimeCategory",
                column: "animeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieMovieCategory_movieCategoryId",
                table: "MovieMovieCategory",
                column: "movieCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TvShowTvShowCategory_showCategoryId",
                table: "TvShowTvShowCategory",
                column: "showCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeAnimeCategory");

            migrationBuilder.DropTable(
                name: "MovieMovieCategory");

            migrationBuilder.DropTable(
                name: "TvShowTvShowCategory");

            migrationBuilder.DropTable(
                name: "AnimeCategory");

            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "MovieCategory");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "TvShowCategory");
        }
    }
}
