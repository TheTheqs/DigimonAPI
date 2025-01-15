// This class is responsible for preparing data to be sent, taking an object and returning structured data ready to be serialized.
namespace DigimonAPI.services;

public static class DF //Stands for Data Formatter
{
	//get structured data from an Digimon object
	public static Object? FormatDigimon(Digimon? digimon)
	{
		try
		{
			if(digimon != null && ValidateDigimon(digimon) && digimon.SpecialMoves != null)
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
					digimon.Id,
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
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Data Formater: " + e.Message);
			return null;
		}
	}
	//get structured data for Special Move
	public static Object? FormatSpecialMove(SpecialMove? sMove)
	{
		try
		{
			if (sMove != null && sMove.Digimons != null)
			{
				ICollection<String> digimons = new HashSet<String>();
				foreach (Digimon digimon in sMove.Digimons)
				{
					if (digimon.Name != null)
					{
						digimons.Add(digimon.Name);
					}
				}
				var jsoned = new
				{
					sMove.Id,
					sMove.Name,
					sMove.Description,
					knownByDigimons = digimons
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
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Data Formater: " + e.Message);
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