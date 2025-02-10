using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging.Modules.User.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "users",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "User_Role",
                schema: "users",
                columns: table => new
                {
                    RolesName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Role", x => new { x.RolesName, x.UserId });
                    table.ForeignKey(
                        name: "FK_User_Role_Roles_RolesName",
                        column: x => x.RolesName,
                        principalSchema: "users",
                        principalTable: "Roles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "Roles",
                column: "Name",
                values: new object[]
                {
                    "Administrator",
                    "Coop",
                    "Reader",
                    "Writer"
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Role_UserId",
                schema: "users",
                table: "User_Role",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Role",
                schema: "users");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "users");
        }
    }
}
