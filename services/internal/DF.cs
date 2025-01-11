// This class is responsible for preparing data to be sent, taking an object and returning structured data ready to be serialized.
namespace DigimonAPI.services;

public static class DF //Stands for Data Formatter
{
	//get structured data from an Digimon object
	public static Object? FormatDigimon(Digimon digimon)
	{
		try
		{
			if(ValidateDigimon(digimon) && digimon.SpecialMoves != null)
			{
				ICollection<String> SpecialMoves = new HashSet<String>();
				foreach(SpecialMove sMove in digimon.SpecialMoves)
				{
					if(sMove.Name != null)
					{
						SpecialMoves.Add(sMove.Name);
					}
				}
				var jsoned = new
				{
					digimon.Name,
					digimon.Description,
					digimon.ImgUrl,
					Type = digimon.Type?.Name,
					Tier = digimon.Tier?.Name,
					Attribute = digimon.Attribute?.Name,
					SpecialMoves
				};
				return jsoned;
			}
			else
			{
				throw new InvalidObjectException();
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return null;
		}
	}

	//Validation function.
	private static bool ValidateDigimon(Digimon digimon)
	{
		return digimon?.Name != null &&
			   digimon.Description != null &&
			   digimon.ImgUrl != null &&
			   digimon.Tier != null &&
			   digimon.Attribute != null &&
			   digimon.Type != null;
	}
}