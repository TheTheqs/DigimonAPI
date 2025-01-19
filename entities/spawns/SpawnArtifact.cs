//This class only holds attributes, a partial class is created to hold its constructors

namespace DigimonAPI.entities;
public partial class SpawnArtifact
{
	[Key]
	public int Id { get; set; }
	[Range(1, 10)]
	public int Tier { get; set; }
	[RegularExpression("^(REGULAR|INITIAL|REWARD)$", ErrorMessage = "Invalid CollectionType")]
	public string? CollectionType { get; set; }
	[Required]
	public int Chance { get; set; }
	public ICollection<Artifact>? Artifacts { get; set; }
	public ICollection<ContentArtifacts>? Contents { get; set; }
}