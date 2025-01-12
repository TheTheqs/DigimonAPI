//This class only holds attributes, a partial class is created to hold its constructors
namespace DigimonAPI.entities;
public partial class Attribute
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(50)]
	public string? Name { get; set; }

	public int? WeakId { get; set; }
	public Attribute? WeakAgainst { get; set; }

	public int? StrongId { get; set; }
	public Attribute? StrongAgainst { get; set; }

	public ICollection<Digimon>? Digimons { get; set; }
}