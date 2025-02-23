using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.Blog.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddContributeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contributes",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributes_Sections_SectionId",
                        column: x => x.SectionId,
                        principalSchema: "Blog",
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contributes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Blog",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContributeContents",
                schema: "Blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributeContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributeContents_Contributes_ContributeId",
                        column: x => x.ContributeId,
                        principalSchema: "Blog",
                        principalTable: "Contributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContributeContents_ContributeId",
                schema: "Blog",
                table: "ContributeContents",
                column: "ContributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributes_SectionId",
                schema: "Blog",
                table: "Contributes",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributes_UserId",
                schema: "Blog",
                table: "Contributes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContributeContents",
                schema: "Blog");

            migrationBuilder.DropTable(
                name: "Contributes",
                schema: "Blog");
        }
    }
}
