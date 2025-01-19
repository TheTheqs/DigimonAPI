//This class only holds attributes, a partial class is created to hold its constructors
//The sum of all chances in spawns must be 100%
namespace DigimonAPI.entities;
public partial class ContentEvents
{
	[Key]
	public int Id { get; set; }
	//A random element will be selected from the spawn collection
	public ICollection<SpawnEvent>? EventsSpawns { get; set; }
	public ICollection<Dungeon>? Dungeons { get; set; }
}