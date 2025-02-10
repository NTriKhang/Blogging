using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.User.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdentityId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                schema: "users",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityId",
                schema: "users",
                table: "Users",
                column: "IdentityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IdentityId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                schema: "users",
                table: "Users");
        }
    }
}
