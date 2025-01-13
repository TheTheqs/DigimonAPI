using System.Xml.Linq;

namespace DigimonAPI.services;
public static class PD //Stands for Populate Database. This class will be used to populate the internal database. Will be deleted after that.
{
	public static async Task<bool> Populate(int index)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][Task] Populate: Starting the process to save Digimon for the specified index: {index}.");
		Digimon? digimon = await DDB.SaveDigimon(await DS.ParseDigimon(index)); //Try to get digimon
		if(digimon != null)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][Task] Populate : the index {index} generated a valid Digimon.");
			Console.WriteLine(digimon.ToString());
			return true;
		}
		else
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Populate : the index {index} cannot generate a valid Digimon. Registering it in the fail log.");
			TC.GerenatePopulateFailure(index);
			return false;
		}
	}
}