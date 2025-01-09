using System.ComponentModel.DataAnnotations;
//This class only holds attributes, a partial class is created to hold its constructors
public partial class Tier
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(50)]
	public string? Name { get; set; }
	public int? Level { get; set; }
	public ICollection<Digimon>? Digimons { get; set; }
}