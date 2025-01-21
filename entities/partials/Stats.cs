//This class only holds the constructors for the relative class
using System.Runtime.CompilerServices;

namespace DigimonAPI.entities;
public partial class Stats
{
	public Stats() { } //An empty constructor is necessary for EF usage

	public Stats(int[] stats) //generic constructor just for primary population.
	{
		if (stats == null || stats.Length != 7) throw new ArgumentNullException("Invalid array");
		this.Association = "TIER1";
		this.Pow = stats[0];
		this.Will = stats[1];
		this.Sta = stats[2];
		this.Res = stats[3];
		this.Spd = stats[4];
		this.Cha = stats[5];
		this.Mhp = stats[6];
	}
	override
	public string ToString()
	{
		int[] stats = { this.Pow, this.Will, this.Sta, this.Res, this.Spd, this.Cha, this.Mhp};
		return $"[ {string.Join(", ", stats)} ]";
	}
}