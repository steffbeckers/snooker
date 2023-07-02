using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snooker.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesOnMatchesDivisionsFrames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DivisionId",
                table: "AppTeams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppDivisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrameCount = table.Column<int>(type: "int", nullable: false),
                    MinPlayerClass = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDivisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLeagues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLeagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HomeTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMatches_AppTeams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "AppTeams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMatches_AppTeams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "AppTeams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppSeasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSeasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchTeamPlayer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCaptain = table.Column<bool>(type: "bit", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamPlayer_AppMatches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "AppMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchTeamPlayer_AppPlayers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AppPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchTeamPlayer_AppTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "AppTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFrames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayScore = table.Column<int>(type: "int", nullable: false),
                    HomePlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeScore = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFrames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFrames_AppMatches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "AppMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFrames_MatchTeamPlayer_AwayPlayerId",
                        column: x => x.AwayPlayerId,
                        principalTable: "MatchTeamPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppFrames_MatchTeamPlayer_HomePlayerId",
                        column: x => x.HomePlayerId,
                        principalTable: "MatchTeamPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTeams_DivisionId",
                table: "AppTeams",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFrames_AwayPlayerId",
                table: "AppFrames",
                column: "AwayPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFrames_HomePlayerId",
                table: "AppFrames",
                column: "HomePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFrames_MatchId",
                table: "AppFrames",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMatches_AwayTeamId",
                table: "AppMatches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMatches_HomeTeamId",
                table: "AppMatches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamPlayer_MatchId",
                table: "MatchTeamPlayer",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamPlayer_PlayerId",
                table: "MatchTeamPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamPlayer_TeamId",
                table: "MatchTeamPlayer",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTeams_AppDivisions_DivisionId",
                table: "AppTeams",
                column: "DivisionId",
                principalTable: "AppDivisions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTeams_AppDivisions_DivisionId",
                table: "AppTeams");

            migrationBuilder.DropTable(
                name: "AppDivisions");

            migrationBuilder.DropTable(
                name: "AppFrames");

            migrationBuilder.DropTable(
                name: "AppLeagues");

            migrationBuilder.DropTable(
                name: "AppSeasons");

            migrationBuilder.DropTable(
                name: "MatchTeamPlayer");

            migrationBuilder.DropTable(
                name: "AppMatches");

            migrationBuilder.DropIndex(
                name: "IX_AppTeams_DivisionId",
                table: "AppTeams");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "AppTeams");
        }
    }
}
