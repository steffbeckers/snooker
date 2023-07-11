using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snooker.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InterclubUpdates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterclubFrames_MatchTeamPlayer_AwayPlayerId",
                table: "InterclubFrames");

            migrationBuilder.DropForeignKey(
                name: "FK_InterclubFrames_MatchTeamPlayer_HomePlayerId",
                table: "InterclubFrames");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchTeamPlayer_InterclubMatches_MatchId",
                table: "MatchTeamPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchTeamPlayer",
                table: "MatchTeamPlayer");

            migrationBuilder.RenameTable(
                name: "MatchTeamPlayer",
                newName: "InterclubMatchTeamPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_MatchTeamPlayer_MatchId",
                table: "InterclubMatchTeamPlayers",
                newName: "IX_InterclubMatchTeamPlayers_MatchId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "InterclubMatchTeamPlayers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterclubMatchTeamPlayers",
                table: "InterclubMatchTeamPlayers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterclubFrames_InterclubMatchTeamPlayers_AwayPlayerId",
                table: "InterclubFrames",
                column: "AwayPlayerId",
                principalTable: "InterclubMatchTeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterclubFrames_InterclubMatchTeamPlayers_HomePlayerId",
                table: "InterclubFrames",
                column: "HomePlayerId",
                principalTable: "InterclubMatchTeamPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterclubMatchTeamPlayers_InterclubMatches_MatchId",
                table: "InterclubMatchTeamPlayers",
                column: "MatchId",
                principalTable: "InterclubMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterclubFrames_InterclubMatchTeamPlayers_AwayPlayerId",
                table: "InterclubFrames");

            migrationBuilder.DropForeignKey(
                name: "FK_InterclubFrames_InterclubMatchTeamPlayers_HomePlayerId",
                table: "InterclubFrames");

            migrationBuilder.DropForeignKey(
                name: "FK_InterclubMatchTeamPlayers_InterclubMatches_MatchId",
                table: "InterclubMatchTeamPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterclubMatchTeamPlayers",
                table: "InterclubMatchTeamPlayers");

            migrationBuilder.RenameTable(
                name: "InterclubMatchTeamPlayers",
                newName: "MatchTeamPlayer");

            migrationBuilder.RenameIndex(
                name: "IX_InterclubMatchTeamPlayers_MatchId",
                table: "MatchTeamPlayer",
                newName: "IX_MatchTeamPlayer_MatchId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "MatchTeamPlayer",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchTeamPlayer",
                table: "MatchTeamPlayer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InterclubFrames_MatchTeamPlayer_AwayPlayerId",
                table: "InterclubFrames",
                column: "AwayPlayerId",
                principalTable: "MatchTeamPlayer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InterclubFrames_MatchTeamPlayer_HomePlayerId",
                table: "InterclubFrames",
                column: "HomePlayerId",
                principalTable: "MatchTeamPlayer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchTeamPlayer_InterclubMatches_MatchId",
                table: "MatchTeamPlayer",
                column: "MatchId",
                principalTable: "InterclubMatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
