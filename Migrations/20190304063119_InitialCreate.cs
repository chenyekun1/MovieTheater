using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    admin_name = table.Column<string>(maxLength: 20, nullable: true),
                    admin_pwd = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "movie_category",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    category_name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "movie_country",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    country_name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_country", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "movie",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    movie_name = table.Column<string>(maxLength: 50, nullable: true),
                    movie_director = table.Column<string>(maxLength: 20, nullable: true),
                    movie_grade = table.Column<string>(nullable: true),
                    movie_actor = table.Column<string>(maxLength: 20, nullable: true),
                    movie_description = table.Column<string>(maxLength: 500, nullable: true),
                    movie_price = table.Column<int>(nullable: false),
                    movie_category_id = table.Column<int>(nullable: true),
                    movie_country_id = table.Column<int>(nullable: true),
                    movie_img = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie", x => x.id);
                    table.ForeignKey(
                        name: "FK_movie_movie_category_movie_category_id",
                        column: x => x.movie_category_id,
                        principalTable: "movie_category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_movie_movie_country_movie_country_id",
                        column: x => x.movie_country_id,
                        principalTable: "movie_country",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movie_movie_category_id",
                table: "movie",
                column: "movie_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_movie_country_id",
                table: "movie",
                column: "movie_country_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "movie");

            migrationBuilder.DropTable(
                name: "movie_category");

            migrationBuilder.DropTable(
                name: "movie_country");
        }
    }
}
