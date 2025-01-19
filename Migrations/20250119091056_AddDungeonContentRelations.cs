using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddDungeonContentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectionType",
                table: "SpawnUsables",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tier",
                table: "SpawnUsables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tier",
                table: "SpawnEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CollectionType",
                table: "SpawnDigimons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tier",
                table: "SpawnDigimons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CollectionType",
                table: "SpawnArtifacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tier",
                table: "SpawnArtifacts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContentArtifacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentArtifacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentDigimons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentDigimons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentUsables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentUsables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtifactsContents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    SpawnId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtifactsContents", x => new { x.ContentId, x.SpawnId });
                    table.ForeignKey(
                        name: "FK_ArtifactsContents_ContentArtifacts_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentArtifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtifactsContents_SpawnArtifacts_SpawnId",
                        column: x => x.SpawnId,
                        principalTable: "SpawnArtifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigimonsContents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    SpawnId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigimonsContents", x => new { x.ContentId, x.SpawnId });
                    table.ForeignKey(
                        name: "FK_DigimonsContents_ContentDigimons_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentDigimons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DigimonsContents_SpawnDigimons_SpawnId",
                        column: x => x.SpawnId,
                        principalTable: "SpawnDigimons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventsContents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    SpawnId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsContents", x => new { x.ContentId, x.SpawnId });
                    table.ForeignKey(
                        name: "FK_EventsContents_ContentEvents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsContents_SpawnEvents_SpawnId",
                        column: x => x.SpawnId,
                        principalTable: "SpawnEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dungeon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ExploreImgUrl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BattleImgUrl = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DigimonsContentId = table.Column<int>(type: "integer", nullable: false),
                    UsablesContentId = table.Column<int>(type: "integer", nullable: false),
                    EventsContentId = table.Column<int>(type: "integer", nullable: false),
                    ArtifactsContentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dungeon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dungeon_ContentArtifacts_ArtifactsContentId",
                        column: x => x.ArtifactsContentId,
                        principalTable: "ContentArtifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dungeon_ContentDigimons_DigimonsContentId",
                        column: x => x.DigimonsContentId,
                        principalTable: "ContentDigimons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dungeon_ContentEvents_EventsContentId",
                        column: x => x.EventsContentId,
                        principalTable: "ContentEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dungeon_ContentUsables_UsablesContentId",
                        column: x => x.UsablesContentId,
                        principalTable: "ContentUsables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsablesContents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "integer", nullable: false),
                    SpawnId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsablesContents", x => new { x.ContentId, x.SpawnId });
                    table.ForeignKey(
                        name: "FK_UsablesContents_ContentUsables_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentUsables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsablesContents_SpawnUsables_SpawnId",
                        column: x => x.SpawnId,
                        principalTable: "SpawnUsables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtifactsContents_SpawnId",
                table: "ArtifactsContents",
                column: "SpawnId");

            migrationBuilder.CreateIndex(
                name: "IX_DigimonsContents_SpawnId",
                table: "DigimonsContents",
                column: "SpawnId");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeon_ArtifactsContentId",
                table: "Dungeon",
                column: "ArtifactsContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeon_DigimonsContentId",
                table: "Dungeon",
                column: "DigimonsContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeon_EventsContentId",
                table: "Dungeon",
                column: "EventsContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dungeon_UsablesContentId",
                table: "Dungeon",
                column: "UsablesContentId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsContents_SpawnId",
                table: "EventsContents",
                column: "SpawnId");

            migrationBuilder.CreateIndex(
                name: "IX_UsablesContents_SpawnId",
                table: "UsablesContents",
                column: "SpawnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtifactsContents");

            migrationBuilder.DropTable(
                name: "DigimonsContents");

            migrationBuilder.DropTable(
                name: "Dungeon");

            migrationBuilder.DropTable(
                name: "EventsContents");

            migrationBuilder.DropTable(
                name: "UsablesContents");

            migrationBuilder.DropTable(
                name: "ContentArtifacts");

            migrationBuilder.DropTable(
                name: "ContentDigimons");

            migrationBuilder.DropTable(
                name: "ContentEvents");

            migrationBuilder.DropTable(
                name: "ContentUsables");

            migrationBuilder.DropColumn(
                name: "CollectionType",
                table: "SpawnUsables");

            migrationBuilder.DropColumn(
                name: "Tier",
                table: "SpawnUsables");

            migrationBuilder.DropColumn(
                name: "Tier",
                table: "SpawnEvents");

            migrationBuilder.DropColumn(
                name: "CollectionType",
                table: "SpawnDigimons");

            migrationBuilder.DropColumn(
                name: "Tier",
                table: "SpawnDigimons");

            migrationBuilder.DropColumn(
                name: "CollectionType",
                table: "SpawnArtifacts");

            migrationBuilder.DropColumn(
                name: "Tier",
                table: "SpawnArtifacts");
        }
    }
}
