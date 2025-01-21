// This class deals with Stats Database CRUD
using Microsoft.EntityFrameworkCore;

namespace DigimonAPI.services;

public static class StatsDB // Stands for Stats Database
{
	// The following function is used to populate the database.
	public static async Task<Stats?> SaveStats(int[] stats)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Stats Database: Generating Stats!");
		try
		{
			if (stats == null)
			{
				throw new ArgumentNullException("stats is null");
			}
			//Object to be saved.
			Stats newStats = new Stats(stats);
			//Pontual instance for the DB connection
			using var context = new AppDbContext();

			if (await context.Stats.AnyAsync(s =>
				s.Pow == newStats.Pow &&
				s.Will == newStats.Will &&
				s.Sta == newStats.Sta &&
				s.Res == newStats.Res &&
				s.Spd == newStats.Spd &&
				s.Cha == newStats.Cha &&
				s.Mhp == newStats.Mhp) == false)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Stats Database: Stats {newStats.ToString()} not found in the database. Requesting creation.");
				await context.Stats.AddAsync(newStats);
				await context.SaveChangesAsync();
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Stats Database: Successfully saved Stats {newStats.ToString()} in the database.");
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Stats Database: {newStats.ToString} already exists. Retrieving from the database.");
			}
			return await context.Stats
					.FirstOrDefaultAsync(s =>
				s.Pow == newStats.Pow &&
				s.Will == newStats.Will &&
				s.Sta == newStats.Sta &&
				s.Res == newStats.Res &&
				s.Spd == newStats.Spd &&
				s.Cha == newStats.Cha &&
				s.Mhp == newStats.Mhp);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Stats Database: Error while retrieving Stats! " + ex.Message);
			return null; // The null response will be well treated by the request
		}
	}
}