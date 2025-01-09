//This class only holds the constructors for the relative class
namespace DigimonAPI.entities;
public partial class Digimon
{
	public Digimon() { } //An empty constructor is necessary for EF usage

	public Digimon(String typeName, String digiDisc, String img, Tier dTier, Type dType, Attribute dAttribute, ICollection<SpecialMove> specialMoves) //generic constructor just for tests. This will be deleted.
	{
		this.Name = typeName;
		this.Description = digiDisc;
		this.ImgUrl = img;
		this.Tier = dTier;
		this.Type = dType;
		this.Attribute = dAttribute;
		this.SpecialMoves = specialMoves;
	}
}