using System.ComponentModel.DataAnnotations;

public partial class SpecialMove
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(50)]
	public string? Name { get; set; }
	public ICollection<Digimon>? Digimons { get; set; }
}