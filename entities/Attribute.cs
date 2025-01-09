using System.ComponentModel.DataAnnotations;

public partial class Attribute
{
	[Key]
	public int Id { get; set; }
	[Required]
	[StringLength(50)]
	public string? Name { get; set; }

	public int? WeakId { get; set; } //aqui precisa haver a chave estrangeira para o id do tipo que ele � fraco
	public Attribute? WeakAgainst { get; set; }

	public int? StringId { get; set; } //aqui precisa haver a chave estrangeira para o tipo que ele � forte
	public Attribute? StrongAgainst { get; set; }

	public ICollection<Digimon>? Digimons { get; set; }
}