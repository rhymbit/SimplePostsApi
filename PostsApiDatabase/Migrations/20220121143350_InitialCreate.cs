using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostsApiDatabase.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    content = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    added = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2022, 1, 21, 14, 33, 50, 145, DateTimeKind.Utc).AddTicks(1320))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts");
        }
    }
}
