//This class only holds attributes, a partial class is created to hold its constructors

using System.Collections.Generic;

namespace DigimonAPI.entities;
public partial class SpawnEvent
{
	[Key]
	public int Id { get; set; }
	[Range(1, 10)]
	public int Tier { get; set; }
	[Required]
	public int Chance { get; set; }
	public ICollection<Event>? Events { get; set; }
	public ICollection<ContentEvents>? Contents { get; set; }
}