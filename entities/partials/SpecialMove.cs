//This class only holds the constructors for the relative class
namespace DigimonAPI.entities;
public partial class SpecialMove
{
	public SpecialMove() { } //An empty constructor is necessary for EF usage

	public SpecialMove(String typeName) //generic constructor just for tests. This will be deleted.
	{
		this.Name = typeName;
	}
}