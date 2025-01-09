//This class only holds the constructors for the relative class
namespace DigimonAPI.entities;
public partial class Type
{
	public Type () {} //An empty constructor is necessary for EF usage

	public Type (String typeName) //generic constructor just for tests. This will be deleted.
	{
		this.Name = typeName;
	}
}