//This class only holds attributes, a partial class is created to hold its constructors
namespace DigimonAPI.entities;
public partial class SpecialMove
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(50)]
	public string? Name { get; set; }
	public string? Description { get; set; }
	public ICollection<Digimon>? Digimons { get; set; }
}