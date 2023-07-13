using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Snooker.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class InterclubUpdates3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterclubTeams_InterclubDivisions_DivisionId",
                table: "InterclubTeams");

            migrationBuilder.AlterColumn<Guid>(
                name: "DivisionId",
                table: "InterclubTeams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InterclubTeams_InterclubDivisions_DivisionId",
                table: "InterclubTeams",
                column: "DivisionId",
                principalTable: "InterclubDivisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterclubTeams_InterclubDivisions_DivisionId",
                table: "InterclubTeams");

            migrationBuilder.AlterColumn<Guid>(
                name: "DivisionId",
                table: "InterclubTeams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_InterclubTeams_InterclubDivisions_DivisionId",
                table: "InterclubTeams",
                column: "DivisionId",
                principalTable: "InterclubDivisions",
                principalColumn: "Id");
        }
    }
}
