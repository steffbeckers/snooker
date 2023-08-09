using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snooker.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InterclubDivisionRoundsDuringSeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoundsPerSeasonCount",
                table: "InterclubDivisions",
                newName: "RoundsDuringSeason");

            migrationBuilder.AddColumn<int>(
                name: "Round",
                table: "InterclubMatches",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Round",
                table: "InterclubMatches");

            migrationBuilder.RenameColumn(
                name: "RoundsDuringSeason",
                table: "InterclubDivisions",
                newName: "RoundsPerSeasonCount");
        }
    }
}
