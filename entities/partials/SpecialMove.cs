//This class only holds the constructors for the relative class
namespace DigimonAPI.entities;
public partial class SpecialMove
{
	public SpecialMove() { } //An empty constructor is necessary for EF usage

	public SpecialMove(String typeName) //generic constructor, used to generate a SpecialObject to be saved in the databank
	{
		this.Name = typeName;
	}
}