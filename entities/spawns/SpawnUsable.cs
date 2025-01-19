//This class only holds attributes, a partial class is created to hold its constructors

using System.Collections.Generic;

namespace DigimonAPI.entities;
public partial class SpawnUsable
{
	[Key]
	public int Id { get; set; }
	[Range(1, 10)]
	public int Tier { get; set; }
	[RegularExpression("^(REGULAR|INITIAL|REWARD)$", ErrorMessage = "Invalid CollectionType")]
	public string? CollectionType { get; set; }
	[Required]
	public int Chance { get; set; }
	public ICollection<Usable>? Usables { get; set; }
	public ICollection<ContentUsables>? Contents { get; set; }
}