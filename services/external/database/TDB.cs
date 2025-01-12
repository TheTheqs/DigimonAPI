//This class deal with Types Database CRUD
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DigimonAPI.services;

public static class TDB //Stands for Type Database
{
	//The following function is used to populate the databank. It will retrieve the matching Type with its ID. It will create a new entity in the database when the object its not there and than retrieve it
	public static async Task<entities.Type?> GenerateType(string? tName)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Type Database: Generating type: {tName}");
		try
		{
			if (tName == null)
			{
				throw new ArgumentNullException("Type name is null");
			}
			//Pontual instance for the DB connection
			using var context = new AppDbContext();

			if (await context.Types.AnyAsync(t => t.Name == tName) == false)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Type Database: {tName} not found in the database. Requesting creation.");
				entities.Type nType = new entities.Type(tName);
				await context.Types.AddAsync(nType);
				await context.SaveChangesAsync();
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Type Database: Successfully saved {tName} in the database.");
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Type Database: {tName} already exists. Retrieving from the database.");
			}
			return await context.Types
					.FirstOrDefaultAsync(t => t.Name == tName);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Type Database: Error while retrieving type: {tName}. " + ex.Message);
			return null; //The null response will be well treated by the request
		}
	}
}