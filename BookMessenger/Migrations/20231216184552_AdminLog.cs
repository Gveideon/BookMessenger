using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMessenger.Migrations
{
    /// <inheritdoc />
    public partial class AdminLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameUser = table.Column<string>(type: "TEXT", nullable: false),
                    RolePrevious = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleCurrent = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminActions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminActions");
        }
    }
}
