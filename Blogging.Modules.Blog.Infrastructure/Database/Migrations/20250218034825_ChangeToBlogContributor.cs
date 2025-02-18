using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.Blog.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToBlogContributor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogUser_Blogs_BlogContributorId",
                schema: "Blog",
                table: "BlogUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogUser_Users_ContributorsId",
                schema: "Blog",
                table: "BlogUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogUser",
                schema: "Blog",
                table: "BlogUser");

            migrationBuilder.RenameTable(
                name: "BlogUser",
                schema: "Blog",
                newName: "BlogContributor",
                newSchema: "Blog");

            migrationBuilder.RenameColumn(
                name: "BlogContributorId",
                schema: "Blog",
                table: "BlogContributor",
                newName: "ContributedBlogsId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogUser_ContributorsId",
                schema: "Blog",
                table: "BlogContributor",
                newName: "IX_BlogContributor_ContributorsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogContributor",
                schema: "Blog",
                table: "BlogContributor",
                columns: new[] { "ContributedBlogsId", "ContributorsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogContributor_Blogs_ContributedBlogsId",
                schema: "Blog",
                table: "BlogContributor",
                column: "ContributedBlogsId",
                principalSchema: "Blog",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogContributor_Users_ContributorsId",
                schema: "Blog",
                table: "BlogContributor",
                column: "ContributorsId",
                principalSchema: "Blog",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogContributor_Blogs_ContributedBlogsId",
                schema: "Blog",
                table: "BlogContributor");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogContributor_Users_ContributorsId",
                schema: "Blog",
                table: "BlogContributor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogContributor",
                schema: "Blog",
                table: "BlogContributor");

            migrationBuilder.RenameTable(
                name: "BlogContributor",
                schema: "Blog",
                newName: "BlogUser",
                newSchema: "Blog");

            migrationBuilder.RenameColumn(
                name: "ContributedBlogsId",
                schema: "Blog",
                table: "BlogUser",
                newName: "BlogContributorId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogContributor_ContributorsId",
                schema: "Blog",
                table: "BlogUser",
                newName: "IX_BlogUser_ContributorsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogUser",
                schema: "Blog",
                table: "BlogUser",
                columns: new[] { "BlogContributorId", "ContributorsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUser_Blogs_BlogContributorId",
                schema: "Blog",
                table: "BlogUser",
                column: "BlogContributorId",
                principalSchema: "Blog",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogUser_Users_ContributorsId",
                schema: "Blog",
                table: "BlogUser",
                column: "ContributorsId",
                principalSchema: "Blog",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
