using System.ComponentModel.DataAnnotations;

public partial class Tier
{
	[Key]
	public int id { get; set; }
	[Required]
	[StringLength(50)]
	public string? name { get; set; }
	public int? level { get; set; }
}