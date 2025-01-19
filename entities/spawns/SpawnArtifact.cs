//This class only holds attributes, a partial class is created to hold its constructors

namespace DigimonAPI.entities;
public partial class SpawnArtifact
{
	[Key]
	public int Id { get; set; }
	[Required]
	public int Chance { get; set; }
	public ICollection<Artifact>? Artifacts { get; set; }
}