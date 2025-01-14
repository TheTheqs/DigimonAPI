//This class deal with Special Move Database CRUD
using Microsoft.EntityFrameworkCore;

namespace DigimonAPI.services;

public static class SDB //Stands for SpecialMoves DataBase
{
	//The following function is used to populate the databank. It will retrieve the matching SpecialMove with its ID. It will create a new entity in the database when the object its not there and than retrieve it
	public static async Task<SpecialMove?> GenerateSpecialMove(string? smName)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Special Move Database: Generating special move: {smName}");
		try
		{
			if (smName == null)
			{
				throw new ArgumentNullException("Special move name is null");
			}
			//Pontual instance for the DB connection
			using var context = new AppDbContext();

			if(await context.SpecialMoves.AnyAsync(s => s.Name == smName) == false)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Special Move Database: {smName} not found in the database. Requesting creation.");
				SpecialMove nSM = new SpecialMove(smName);
				await context.SpecialMoves.AddAsync(nSM);
				await context.SaveChangesAsync();
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Special Move Database: Successfully saved {smName} in the database.");
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Special Move Database: {smName} already exists. Retrieving from the database.");
			}
			return await context.SpecialMoves
					.FirstOrDefaultAsync(s => s.Name == smName);
		} catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Special Move Database: Error while retrieving special move: {smName}. " + ex.Message);
			return null; //The null response will be well treated by the request
		}
	}
	//Get smove by id
	public static async Task<SpecialMove?> GetSpecialMoveById(int Id)
	{
		try
		{
			if (Id <= 0 || Id >= 2011)
			{
				throw new ArgumentOutOfRangeException(nameof(Id), "Invalid ID. Argument must be an integer between 1 and 2010.");
			}

			// Pontual instance for the DB connection
			using var context = new AppDbContext();
			return await context.SpecialMoves
				.Include(d => d.Digimons)
				.FirstOrDefaultAsync(s => s.Id == Id);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Special Move Database: Error while retrieving Special Move: Id = {Id}. " + ex.Message);
			return null;
		}
	}
}