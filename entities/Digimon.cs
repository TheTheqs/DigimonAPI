using System.ComponentModel.DataAnnotations;

public partial class Digimon
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(100)]
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? ImgUrl { get; set; }
	public int? TierId { get; set; }
	public Tier? Tier { get; set; }
	public int? TypeId { get; set; }
	public Type? Type { get; set; }
	public int? AttributeId { get; set; }
	public Attribute? Attribute { get; set; }
	public Array[]? SpecialMoves { get; set; }
}