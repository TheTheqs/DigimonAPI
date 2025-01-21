using System.ComponentModel.DataAnnotations.Schema;

namespace DigimonAPI.entities;

public partial class Stats
{
	[Key]
	public int Id { get; set; }
	[RegularExpression("^(TIER1|TIER2|TIER3|TIER4|TIER5|TIER6|ITEM1|ITEM2|ITEM3)$", ErrorMessage = "Invalid Association")]
	public string? Association { get; set; }
	[Range(1, 3)]
	public int Pow {  get; set; }
	[Range(1, 3)]
	public int Will { get; set; }
	[Range(1, 3)]
	public int Sta { get; set; }
	[Range(1, 3)]
	public int Res { get; set; }
	[Range(1, 3)]
	public int Spd { get; set; }
	[Range(1, 3)]
	public int Cha { get; set; }
	[Range(1, 3)]
	public int Mhp { get; set; }
	public int? DigimonId { get; set; }
	public Digimon? Digimon { get; set; }
	public int? ArtifactId { get; set; }
	public Artifact? Artifact {  get; set; }
}