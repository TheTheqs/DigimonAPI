using System.ComponentModel.DataAnnotations;

public partial class Type
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(100)]
	public string? Name { get; set; }
	public ICollection<Digimon>? Digimons { get; set; }
}