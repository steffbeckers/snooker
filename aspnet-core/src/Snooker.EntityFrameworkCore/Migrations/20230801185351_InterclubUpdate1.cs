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
            migrationBuilder.AddColumn<int>(
                name: "RoundsPerSeasonCount",
                table: "InterclubDivisions",
                type: "int",
                nullable: true,
                defaultValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoundsPerSeasonCount",
                table: "InterclubDivisions");
        }
    }
}
