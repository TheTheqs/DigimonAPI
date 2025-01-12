// This class deals with Tiers Database CRUD
using Microsoft.EntityFrameworkCore;

namespace DigimonAPI.services;

public static class TierDB // Stands for Tier Database
{
	// The following function is used to populate the database.
	// It will retrieve the matching Tier with its ID or create a new entity if it's not there and then retrieve it.
	public static async Task<entities.Tier?> GenerateTier(string? tierName)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Tier Database: Generating tier: {tierName}");
		try
		{
			if (tierName == null)
			{
				throw new ArgumentNullException("Tier name is null");
			}
			// Pontual instance for the DB connection
			using var context = new AppDbContext();

			if (await context.Tiers.AnyAsync(t => t.Name == tierName) == false)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Tier Database: {tierName} not found in the database. Requesting creation.");
				entities.Tier newTier = new entities.Tier(tierName);
				await context.Tiers.AddAsync(newTier);
				await context.SaveChangesAsync();
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Tier Database: Successfully saved {tierName} in the database.");
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Tier Database: {tierName} already exists. Retrieving from the database.");
			}
			return await context.Tiers
					.FirstOrDefaultAsync(t => t.Name == tierName);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Tier Database: Error while retrieving tier: {tierName}. " + ex.Message);
			return null; // The null response will be well treated by the request
		}
	}
}