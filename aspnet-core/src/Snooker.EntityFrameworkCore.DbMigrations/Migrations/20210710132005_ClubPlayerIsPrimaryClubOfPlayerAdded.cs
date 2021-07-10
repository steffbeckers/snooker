using Microsoft.EntityFrameworkCore.Migrations;

namespace Snooker.Migrations
{
    public partial class ClubPlayerIsPrimaryClubOfPlayerAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryClubOfPlayer",
                table: "AppClubPlayers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimaryClubOfPlayer",
                table: "AppClubPlayers");
        }
    }
}
