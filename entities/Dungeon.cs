//This class only holds attributes, a partial class is created to hold its constructors
namespace DigimonAPI.entities;
public partial class Dungeon
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(50)]
	public string? Name { get; set; }
	[StringLength(50)]
	public string? ExploreImgUrl { get; set; }
	[StringLength(50)]
	public string? BattleImgUrl { get; set; }
	public int DigimonsContentId { get; set; }
	public ContentDigimons? Digimons { get; set; }
	public int UsablesContentId { get; set; }
	public ContentUsables? Usables { get; set; }
	public int EventsContentId { get; set; }
	public ContentEvents? Events { get; set; }
	public int ArtifactsContentId { get; set; }
	public ContentArtifacts? Artifacts { get; set; }
}