using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snooker.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Interclub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterclubClubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
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
                    table.PrimaryKey("PK_InterclubClubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterclubSeasons",
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
                    table.PrimaryKey("PK_InterclubSeasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterclubClubAddresses",
                columns: table => new
                {
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterclubClubAddresses", x => x.ClubId);
                    table.ForeignKey(
                        name: "FK_InterclubClubAddresses_InterclubClubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "InterclubClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterclubPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: true),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProfilePictureFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_InterclubPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterclubPlayers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InterclubPlayers_InterclubClubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "InterclubClubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterclubDivisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrameCount = table.Column<int>(type: "int", nullable: false),
                    MinPlayerClass = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_InterclubDivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterclubDivisions_InterclubSeasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "InterclubSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterclubTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_InterclubTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterclubTeams_InterclubClubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "InterclubClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterclubTeams_InterclubDivisions_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "InterclubDivisions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterclubMatches",
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
                    table.PrimaryKey("PK_InterclubMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterclubMatches_InterclubTeams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "InterclubTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterclubMatches_InterclubTeams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "InterclubTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterclubTeamPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCaptain = table.Column<bool>(type: "bit", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_InterclubTeamPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterclubTeamPlayers_InterclubPlayers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "InterclubPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterclubTeamPlayers_InterclubTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "InterclubTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchTeamPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchTeamPlayer_InterclubMatches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "InterclubMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterclubFrames",
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
                    table.PrimaryKey("PK_InterclubFrames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterclubFrames_InterclubMatches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "InterclubMatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterclubFrames_MatchTeamPlayer_AwayPlayerId",
                        column: x => x.AwayPlayerId,
                        principalTable: "MatchTeamPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterclubFrames_MatchTeamPlayer_HomePlayerId",
                        column: x => x.HomePlayerId,
                        principalTable: "MatchTeamPlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterclubDivisions_SeasonId",
                table: "InterclubDivisions",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubFrames_AwayPlayerId",
                table: "InterclubFrames",
                column: "AwayPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubFrames_HomePlayerId",
                table: "InterclubFrames",
                column: "HomePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubFrames_MatchId",
                table: "InterclubFrames",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubMatches_AwayTeamId",
                table: "InterclubMatches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubMatches_HomeTeamId",
                table: "InterclubMatches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubPlayers_ClubId",
                table: "InterclubPlayers",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubPlayers_UserId",
                table: "InterclubPlayers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubTeamPlayers_PlayerId",
                table: "InterclubTeamPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubTeamPlayers_TeamId",
                table: "InterclubTeamPlayers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubTeams_ClubId",
                table: "InterclubTeams",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_InterclubTeams_DivisionId",
                table: "InterclubTeams",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchTeamPlayer_MatchId",
                table: "MatchTeamPlayer",
                column: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterclubClubAddresses");

            migrationBuilder.DropTable(
                name: "InterclubFrames");

            migrationBuilder.DropTable(
                name: "InterclubTeamPlayers");

            migrationBuilder.DropTable(
                name: "MatchTeamPlayer");

            migrationBuilder.DropTable(
                name: "InterclubPlayers");

            migrationBuilder.DropTable(
                name: "InterclubMatches");

            migrationBuilder.DropTable(
                name: "InterclubTeams");

            migrationBuilder.DropTable(
                name: "InterclubClubs");

            migrationBuilder.DropTable(
                name: "InterclubDivisions");

            migrationBuilder.DropTable(
                name: "InterclubSeasons");
        }
    }
}
