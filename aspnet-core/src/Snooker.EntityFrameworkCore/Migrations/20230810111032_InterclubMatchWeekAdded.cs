using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snooker.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InterclubMatchWeekAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredMatchDay",
                table: "InterclubTeams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "InterclubMatches",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredMatchDay",
                table: "InterclubTeams");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "InterclubMatches");
        }
    }
}
