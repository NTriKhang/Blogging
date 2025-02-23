using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.Blog.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropToContributeAndBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                schema: "Blog",
                table: "Contributes",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "Blog",
                table: "Contributes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                schema: "Blog",
                table: "Contributes");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "Blog",
                table: "Contributes");
        }
    }
}
