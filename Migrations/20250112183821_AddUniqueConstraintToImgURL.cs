using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToImgURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Digimons_ImgUrl",
                table: "Digimons",
                column: "ImgUrl",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Attributes_StrongId",
                table: "Attributes",
                column: "StrongId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Attributes_StrongId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Digimons_ImgUrl",
                table: "Digimons");

            migrationBuilder.RenameColumn(
                name: "StrongId",
                table: "Attributes",
                newName: "StringId");

            migrationBuilder.RenameIndex(
                name: "IX_Attributes_StrongId",
                table: "Attributes",
                newName: "IX_Attributes_StringId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Attributes_StringId",
                table: "Attributes",
                column: "StringId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
