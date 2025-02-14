using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.Blog.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "text", nullable: false),
                    Like = table.Column<int>(type: "integer", nullable: false),
                    Dislike = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "Blog");
        }
    }
}
