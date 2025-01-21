using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class StatsClassandRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Association = table.Column<string>(type: "text", nullable: true),
                    Pow = table.Column<int>(type: "integer", nullable: false),
                    Will = table.Column<int>(type: "integer", nullable: false),
                    Sta = table.Column<int>(type: "integer", nullable: false),
                    Res = table.Column<int>(type: "integer", nullable: false),
                    Spd = table.Column<int>(type: "integer", nullable: false),
                    Cha = table.Column<int>(type: "integer", nullable: false),
                    Mhp = table.Column<int>(type: "integer", nullable: false),
                    DigimonId = table.Column<int>(type: "integer", nullable: true),
                    ArtifactId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stats_Artifacts_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "Artifacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stats_Digimons_DigimonId",
                        column: x => x.DigimonId,
                        principalTable: "Digimons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stats_ArtifactId",
                table: "Stats",
                column: "ArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_Stats_DigimonId",
                table: "Stats",
                column: "DigimonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stats");
        }
    }
}
