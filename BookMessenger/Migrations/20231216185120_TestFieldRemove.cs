using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMessenger.Migrations
{
    /// <inheritdoc />
    public partial class TestFieldRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestField",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestField",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
