//This class only holds attributes, a partial class is created to hold its constructors

using System.Collections.Generic;

namespace DigimonAPI.entities;
public partial class SpawnUsable
{
	[Key]
	public int Id { get; set; }
	[Required]
	public int Chance { get; set; }
	public ICollection<Usable>? Usables { get; set; }
}