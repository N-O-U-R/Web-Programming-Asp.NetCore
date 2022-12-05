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
                    animeStory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    animeCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.animeId);
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
                    movieRunningTime = table.Column<int>(type: "int", nullable: false),
                    movieCategory = table.Column<int>(type: "int", nullable: false)
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
                    showStory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    showCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.showId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Shows");
        }
    }
}
