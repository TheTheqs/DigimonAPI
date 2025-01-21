// This class is responsible for preparing data to be sent, taking an object and returning structured data ready to be serialized.
using DigimonAPI.entities;
using System.Collections.Generic;

namespace DigimonAPI.services;

public static class DF //Stands for Data Formatter
{
	//List Formating
	public static Object? FormatList<T>(List<T>? list)
	{
		try
		{
			if (list == null || !list.Any())
			{
				throw new Exception("Input list is null or empty");
			}

			ICollection<string> result = new HashSet<string>();

			// Iterate over the list and dynamically check for the 'Name' property
			foreach (T item in list)
			{
				var nameProperty = item?.GetType().GetProperty("Name");
				if (nameProperty != null)
				{
					var nameValue = nameProperty.GetValue(item) as string;
					if (!string.IsNullOrEmpty(nameValue))
					{
						result.Add(nameValue);
					}
				}
			}

			// Dynamically determine the name of the class for the output object
			string className = typeof(T).Name + "s"; // Pluralize the class name

			var jsoned = new Dictionary<string, ICollection<string>>
			{
				{ className, result }
			};

			return jsoned;
		}
		catch (Exception e)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Data Formatter: " + e.Message);
			return null;
		}
	}
	//Format Attribute
	public static Object? FormatAttribute(DigimonAPI.entities.Attribute? attribute)
	{
		try
		{
			if (attribute != null)
			{
				var jsoned = new
				{
					attribute.Id,
					attribute.Name,
					WeakAgainst = attribute.WeakAgainst?.Name,
					StrongAgainst = attribute.StrongAgainst?.Name,
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
	//format attribute list
	public static List<Object?>? FormatAttributeList(List<DigimonAPI.entities.Attribute>? aList)
	{
		try
		{
			if (aList == null || !aList.Any())
			{
				throw new Exception("Input list is null or empty");
			}
			ICollection<Object?> result = new HashSet<Object?>();
			foreach (var attribute in aList)
			{
				if(attribute != null)
				{
					result.Add(FormatAttribute(attribute));
				}
			}
			return result.ToList();
		}
		catch (Exception e)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Data Formater: " + e.Message);
			return null;
		}
	}
	//Format Tier
	public static Object? FormatTier(Tier? tier)
	{
		try
		{
			if (tier != null)
			{
				var jsoned = new
				{
					tier.Id,
					tier.Name
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
	//Format Digimon
	public static Object? FormatDigimon(Digimon? digimon)
	{
		try
		{
			if(digimon != null && ValidateDigimon(digimon) && digimon.SpecialMoves != null)
			{
				ICollection<Object?> SpecialMoves = new HashSet<Object?>();
				foreach(SpecialMove sMove in digimon.SpecialMoves)
				{
					if(sMove.Name != null)
					{
						SpecialMoves.Add(FormatSpecialMove(sMove));
					}
				}
				string stats = "";
				if(digimon.Stats != null && digimon.Stats.Count > 0)
				{
					foreach(Stats stat  in digimon.Stats)
					{
						stats += stat.ToString() + "\n";
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
					SpecialMoves,
					BaseStatus = stats
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
	//Format Digimon list
	public static Object? FormatDigimonList(List<Digimon> digimons)
	{
		try
		{
			if (digimons != null && digimons.Count > 0)
			{
				List<Object> jsoned = new List<Object>();
				foreach(Digimon digimon in digimons)
				{
					var digi = FormatDigimon(digimon);
					if(digi != null)
					{
						jsoned.Add(digi);
					}
				}
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
	//Format special move
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