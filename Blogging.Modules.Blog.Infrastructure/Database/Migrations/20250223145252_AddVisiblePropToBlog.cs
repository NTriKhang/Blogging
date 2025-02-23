using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.Blog.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddVisiblePropToBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInternalVisible",
                schema: "Blog",
                table: "Blogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublicVisible",
                schema: "Blog",
                table: "Blogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInternalVisible",
                schema: "Blog",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "IsPublicVisible",
                schema: "Blog",
                table: "Blogs");
        }
    }
}
