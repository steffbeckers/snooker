using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snooker.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InterclubUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomeScore",
                table: "InterclubFrames",
                newName: "HomePlayerScore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomePlayerScore",
                table: "InterclubFrames",
                newName: "HomeScore");
        }
    }
}
