// This class deals with Attributes Database CRUD
using Microsoft.EntityFrameworkCore;

namespace DigimonAPI.services;

public static class ADB // Stands for Attribute Database
{
	// The following function is used to populate the database. 
	// It will retrieve the matching Attribute with its ID or create a new entity if it's not there and then retrieve it.
	public static async Task<entities.Attribute?> GenerateAttribute(string? attrName)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Attribute Database: Generating attribute: {attrName}");
		try
		{
			if (attrName == null)
			{
				throw new ArgumentNullException("Attribute name is null");
			}
			// Pontual instance for the DB connection
			using var context = new AppDbContext();

			if (await context.Attributes.AnyAsync(a => a.Name == attrName) == false)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Attribute Database: {attrName} not found in the database. Requesting creation.");
				entities.Attribute nAttribute = new entities.Attribute(attrName);
				await context.Attributes.AddAsync(nAttribute);
				await context.SaveChangesAsync();
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Attribute Database: Successfully saved {attrName} in the database.");
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Attribute Database: {attrName} already exists. Retrieving from the database.");
			}
			return await context.Attributes
					.FirstOrDefaultAsync(a => a.Name == attrName);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Attribute Database: Error while retrieving attribute: {attrName}. " + ex.Message);
			return null; // The null response will be well treated by the request
		}
	}
}