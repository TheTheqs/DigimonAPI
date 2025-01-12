using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddNewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DigimonSpecialMove_Digimons_DigimonsId",
                table: "DigimonSpecialMove");

            migrationBuilder.DropForeignKey(
                name: "FK_DigimonSpecialMove_SpecialMoves_SpecialMovesId",
                table: "DigimonSpecialMove");

            migrationBuilder.RenameColumn(
                name: "SpecialMovesId",
                table: "DigimonSpecialMove",
                newName: "SpecialMoveId");

            migrationBuilder.RenameColumn(
                name: "DigimonsId",
                table: "DigimonSpecialMove",
                newName: "DigimonId");

            migrationBuilder.RenameIndex(
                name: "IX_DigimonSpecialMove_SpecialMovesId",
                table: "DigimonSpecialMove",
                newName: "IX_DigimonSpecialMove_SpecialMoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_DigimonSpecialMove_Digimons_DigimonId",
                table: "DigimonSpecialMove",
                column: "DigimonId",
                principalTable: "Digimons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DigimonSpecialMove_SpecialMoves_SpecialMoveId",
                table: "DigimonSpecialMove",
                column: "SpecialMoveId",
                principalTable: "SpecialMoves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DigimonSpecialMove_Digimons_DigimonId",
                table: "DigimonSpecialMove");

            migrationBuilder.DropForeignKey(
                name: "FK_DigimonSpecialMove_SpecialMoves_SpecialMoveId",
                table: "DigimonSpecialMove");

            migrationBuilder.RenameColumn(
                name: "SpecialMoveId",
                table: "DigimonSpecialMove",
                newName: "SpecialMovesId");

            migrationBuilder.RenameColumn(
                name: "DigimonId",
                table: "DigimonSpecialMove",
                newName: "DigimonsId");

            migrationBuilder.RenameIndex(
                name: "IX_DigimonSpecialMove_SpecialMoveId",
                table: "DigimonSpecialMove",
                newName: "IX_DigimonSpecialMove_SpecialMovesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DigimonSpecialMove_Digimons_DigimonsId",
                table: "DigimonSpecialMove",
                column: "DigimonsId",
                principalTable: "Digimons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DigimonSpecialMove_SpecialMoves_SpecialMovesId",
                table: "DigimonSpecialMove",
                column: "SpecialMovesId",
                principalTable: "SpecialMoves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
