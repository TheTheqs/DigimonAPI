using System.ComponentModel.DataAnnotations;

public partial class Type
{
	[Key]
	public int id { get; set; }
	[Required]
	[StringLength(100)]
	public string? name { get; set; }
}