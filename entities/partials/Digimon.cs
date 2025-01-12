//This class only holds the constructors for the relative class
namespace DigimonAPI.entities;
public partial class Digimon
{
	public Digimon() { } //An empty constructor is necessary for EF usage

	public Digimon(String typeName, String digiDisc, String img, Tier dTier, Type dType, Attribute dAttribute, ICollection<SpecialMove> specialMoves) //generic constructor just for DS class generator
	{
		this.Name = typeName;
		this.Description = digiDisc;
		this.ImgUrl = img;
		this.Tier = dTier;
		this.Type = dType;
		this.Attribute = dAttribute;
		this.SpecialMoves = specialMoves;
	}

	//This constructor is specific for the DB insertion
	public Digimon(Digimon nDigimon)
	{
		this.Name = nDigimon.Name;
		this.Description = nDigimon.Description;
		this.ImgUrl = nDigimon.ImgUrl;
		if (nDigimon.Tier != null && nDigimon.Type != null && nDigimon.Attribute != null && nDigimon.SpecialMoves != null)
		{
			this.TierId = nDigimon.Tier.Id;
			this.TypeId = nDigimon.Type.Id;
			this.AttributeId = nDigimon.Attribute.Id;
			this.SpecialMoves = nDigimon.SpecialMoves;
		}
		//This is optional, just to make sure these attributes is null
		this.Tier = null;
		this.Attribute = null;
		this.Type = null;
	}
}