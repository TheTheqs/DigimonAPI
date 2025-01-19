//This class only holds attributes, a partial class is created to hold its constructors
namespace DigimonAPI.entities;
public partial class ContentArtifacts
{
	[Key]
	public int Id { get; set; }
	//A random element will be selected from the spawn collection
	public ICollection<SpawnArtifact>? ArtifactsSpawns { get; set; }
	public ICollection<Dungeon>? Dungeons { get; set; }
}