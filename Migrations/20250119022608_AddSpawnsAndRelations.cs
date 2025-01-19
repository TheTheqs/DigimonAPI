using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddSpawnsAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artifacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpawnArtifacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnArtifacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpawnDigimons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnDigimons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpawnEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpawnUsables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Chance = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnUsables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtifactSpawn",
                columns: table => new
                {
                    ArtifactId = table.Column<int>(type: "integer", nullable: false),
                    SpawnArtifactId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactSpawn", x => new { x.ArtifactId, x.SpawnArtifactId });
                    table.ForeignKey(
                        name: "FK_ArtifactSpawn_Artifacts_ArtifactId",
                        column: x => x.ArtifactId,
                        principalTable: "Artifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtifactSpawn_SpawnArtifacts_SpawnArtifactId",
                        column: x => x.SpawnArtifactId,
                        principalTable: "SpawnArtifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigimonSpawn",
                columns: table => new
                {
                    DigimonId = table.Column<int>(type: "integer", nullable: false),
                    SpawnDigimonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigimonSpawn", x => new { x.DigimonId, x.SpawnDigimonId });
                    table.ForeignKey(
                        name: "FK_DigimonSpawn_Digimons_DigimonId",
                        column: x => x.DigimonId,
                        principalTable: "Digimons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigimonSpawn_SpawnDigimons_SpawnDigimonId",
                        column: x => x.SpawnDigimonId,
                        principalTable: "SpawnDigimons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSpawn",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    SpawnEventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpawn", x => new { x.EventId, x.SpawnEventId });
                    table.ForeignKey(
                        name: "FK_EventSpawn_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventSpawn_SpawnEvents_SpawnEventId",
                        column: x => x.SpawnEventId,
                        principalTable: "SpawnEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsableSpawn",
                columns: table => new
                {
                    SpawnUsableId = table.Column<int>(type: "integer", nullable: false),
                    UsableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsableSpawn", x => new { x.SpawnUsableId, x.UsableId });
                    table.ForeignKey(
                        name: "FK_UsableSpawn_SpawnUsables_SpawnUsableId",
                        column: x => x.SpawnUsableId,
                        principalTable: "SpawnUsables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsableSpawn_Usables_UsableId",
                        column: x => x.UsableId,
                        principalTable: "Usables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactSpawn_SpawnArtifactId",
                table: "ArtifactSpawn",
                column: "SpawnArtifactId");

            migrationBuilder.CreateIndex(
                name: "IX_DigimonSpawn_SpawnDigimonId",
                table: "DigimonSpawn",
                column: "SpawnDigimonId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpawn_SpawnEventId",
                table: "EventSpawn",
                column: "SpawnEventId");

            migrationBuilder.CreateIndex(
                name: "IX_UsableSpawn_UsableId",
                table: "UsableSpawn",
                column: "UsableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtifactSpawn");

            migrationBuilder.DropTable(
                name: "DigimonSpawn");

            migrationBuilder.DropTable(
                name: "EventSpawn");

            migrationBuilder.DropTable(
                name: "UsableSpawn");

            migrationBuilder.DropTable(
                name: "Artifacts");

            migrationBuilder.DropTable(
                name: "SpawnArtifacts");

            migrationBuilder.DropTable(
                name: "SpawnDigimons");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "SpawnEvents");

            migrationBuilder.DropTable(
                name: "SpawnUsables");

            migrationBuilder.DropTable(
                name: "Usables");
        }
    }
}
