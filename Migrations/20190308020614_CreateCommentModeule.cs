using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTheater.Migrations
{
    public partial class CreateCommentModeule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customer_name = table.Column<string>(maxLength: 20, nullable: true),
                    customer_pwd = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    commentId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customer_id = table.Column<int>(nullable: false),
                    movie_id = table.Column<int>(nullable: false),
                    comment_context = table.Column<string>(maxLength: 300, nullable: true),
                    comment_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_comment_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_movie_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_customer_id",
                table: "comment",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_movie_id",
                table: "comment",
                column: "movie_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
