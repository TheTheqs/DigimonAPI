//This class only holds attributes, a partial class is created to hold its constructors
namespace DigimonAPI.entities;
public partial class ContentDigimons
{
	[Key]
	public int Id { get; set; }
	//A random element will be selected from the spawn collection
	public ICollection<SpawnDigimon>? DigimonsSpawns { get; set; }
	public ICollection<Dungeon>? Dungeons { get; set; }
}