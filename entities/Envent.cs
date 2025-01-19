//This class only holds attributes, a partial class is created to hold its constructors
namespace DigimonAPI.entities;
public partial class Event
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(30)]
	public string? Name { get; set; }

	public string? Description { get; set; }
	public ICollection<SpawnEvent>? Spawns { get; set; }
}