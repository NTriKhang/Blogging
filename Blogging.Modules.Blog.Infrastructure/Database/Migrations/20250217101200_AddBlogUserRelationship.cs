using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.Blog.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogUser",
                schema: "Blog",
                columns: table => new
                {
                    BlogContributorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContributorsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogUser", x => new { x.BlogContributorId, x.ContributorsId });
                    table.ForeignKey(
                        name: "FK_BlogUser_Blogs_BlogContributorId",
                        column: x => x.BlogContributorId,
                        principalSchema: "Blog",
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogUser_Users_ContributorsId",
                        column: x => x.ContributorsId,
                        principalSchema: "Blog",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogUser_ContributorsId",
                schema: "Blog",
                table: "BlogUser",
                column: "ContributorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogUser",
                schema: "Blog");
        }
    }
}
