// This class deals with Digimon Database CRUD
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DigimonAPI.services;

public static class DDB // Stands for Digimon Database
{
	// The following function is used to populate the database. 
	// It will receive a Digimon object from de DS class, validate every attribute and then it will try to add it to the database.
	public static async Task<Digimon?> SaveDigimon(Digimon? nDigimon)
	{
		try
		{
			if (nDigimon == null)
			{
				throw new ArgumentNullException("Digimon is null");
			}
			using var context = new AppDbContext(); //Pontual context variable
			if (await context.Digimons.AnyAsync(d => d.ImgUrl == nDigimon.ImgUrl) == false)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: New digimon found, starting digimon insertion: {nDigimon.Name}");
				//Basic attributes validation (Name, img, desc)
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Validating Digimon Basics(Name, imgUrl, Description):  {nDigimon.ImgUrl}");
				if (!ValidateDigimon(nDigimon))
				{
					throw new Exception("Invalid argment. Some Digimon attributes are null!");
				}
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Digimon Basics OK");
				//Tier validation
				if (nDigimon.Tier != null)
				{
					Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Validating Digimon Tier:  {nDigimon.Tier.Name}");
					nDigimon.Tier = await TierDB.GenerateTier(nDigimon.Tier.Name);

					if (nDigimon.Tier == null)
					{
						throw new Exception("Failed to generate a valid Tier for the Digimon. Tier is null!");
					}
				}
				else
				{
					throw new Exception("Digimon Tier was not provided (null) and cannot be processed!");
				}
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Digimon Tier OK");
				//Type validation
				if (nDigimon.Type != null)
				{
					Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Validating Digimon Type:  {nDigimon.Type.Name}");
					nDigimon.Type = await TDB.GenerateType(nDigimon.Type.Name);

					if (nDigimon.Type == null)
					{
						throw new Exception("Failed to generate a valid Type for the Digimon. Type is null!");
					}
				}
				else
				{
					throw new Exception("Digimon Tier was not provided (null) and cannot be processed!");
				}
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Digimon Type OK");
				//Attribute Validation
				if (nDigimon.Attribute != null)
				{
					Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Validating Digimon Attribute:  {nDigimon.Attribute.Name}");
					nDigimon.Attribute = await ADB.GenerateAttribute(nDigimon.Attribute.Name);

					if (nDigimon.Attribute == null)
					{
						throw new Exception("Failed to generate a valid Attribute for the Digimon. Tier is null!");
					}
				}
				else
				{
					throw new Exception("Digimon Tier was not provided (null) and cannot be processed!");
				}
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Digimon Attribute OK");
				// Special Moves validation
				if (nDigimon.SpecialMoves != null)
				{
					var updatedMoves = new List<SpecialMove>(); // Convert to a list to allow modifying the elements
					Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Validating Digimon Special moves: Number of Special Moves = {nDigimon.SpecialMoves.Count}");
					foreach (var move in nDigimon.SpecialMoves)
					{
						if (move != null)
						{
							// Try to get the current move in the database
							var existingMove = await context.SpecialMoves
								.FirstOrDefaultAsync(sm => sm.Name == move.Name);

							if (existingMove != null)
							{
								context.Attach(existingMove);
								updatedMoves.Add(existingMove);
							}
							else
							{
								var newMove = new SpecialMove { Name = move.Name };
								await context.SpecialMoves.AddAsync(newMove);
								await context.SaveChangesAsync();
								updatedMoves.Add(newMove);
							}
						}
						else
						{
							throw new Exception("A move in the Digimon list has a null value");
						}
					}
					nDigimon.SpecialMoves = updatedMoves;
				}
				else
				{
					throw new Exception("Digimon object has a null Special Move Collection");
				}

				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Digimon Special Moves OK");
				//Getting the insertable Digimon Object
				Digimon newDigimon = new Digimon(nDigimon);
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Getting insertable Digimon");
				if (ValidateForDatabaseInsertion(newDigimon))
				{
					Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Insertable digimon OK, Requesting insertion...");
					await context.Digimons.AddAsync(newDigimon);
					await context.SaveChangesAsync();
					Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: Successfully saved {newDigimon.Name} in the database.");
				}
				else
				{
					throw new Exception("Generated Digimon is invalid");
				}
				
			}
			else
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Digimon Database: {nDigimon.Name} already exists. Retrieving from the database.");
			}
			return await context.Digimons
						.Include(d => d.Tier)              
						.Include(d => d.Type)              
						.Include(d => d.Attribute)         
						.Include(d => d.SpecialMoves)      
						.FirstOrDefaultAsync(d => d.ImgUrl == nDigimon.ImgUrl);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Digimon Database: {ex.Message}");
			if (ex.InnerException != null)
			{
				Console.WriteLine($"[Inner Exception] {ex.InnerException.Message}");
			}
			return null; // The null response will be well treated by the request
		}
	}

	// This function validates only the basic (non-object) attributes of a Digimon.
	private static bool ValidateDigimon(Digimon digimon)
	{
		if (digimon == null ||
			digimon.Name == null ||
			digimon.Description == null ||
			digimon.ImgUrl == null)
		{
			return false;
		}
		return true;
	}
	//This function validates each necessary attribute before DB insertion
	public static bool ValidateForDatabaseInsertion(Digimon digimon)
	{
		// Verifica os atributos básicos
		if (string.IsNullOrWhiteSpace(digimon.Name))
		{
			Console.WriteLine("[Validation] Digimon name is invalid (null or empty).");
			return false;
		}

		if (string.IsNullOrWhiteSpace(digimon.Description))
		{
			Console.WriteLine("[Validation] Digimon description is invalid (null or empty).");
			return false;
		}

		if (string.IsNullOrWhiteSpace(digimon.ImgUrl))
		{
			Console.WriteLine("[Validation] Digimon image URL is invalid (null or empty).");
			return false;
		}

		//Foreigner keys
		if (digimon.TierId == null)
		{
			Console.WriteLine("[Validation] Digimon TierId is null.");
			return false;
		}

		if (digimon.TypeId == null)
		{
			Console.WriteLine("[Validation] Digimon TypeId is null.");
			return false;
		}

		if (digimon.AttributeId == null)
		{
			Console.WriteLine("[Validation] Digimon AttributeId is null.");
			return false;
		}

		// Special moves
		if (digimon.SpecialMoves == null || !digimon.SpecialMoves.Any())
		{
			Console.WriteLine("[Validation] Digimon SpecialMoves collection is null or empty.");
			return false;
		}

		// Return true if everything is okay
		return true;
	}

	//This function will provide the max id from the digimon table.
	public static async Task<int> GetMaxIdAsync()
	{
		try
		{
			using var context = new AppDbContext(); //Pontual context variable

			return await context.Digimons.MaxAsync(d => (int?)d.Id) ?? 0;

		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Digimon Database: {ex.Message}");
			if (ex.InnerException != null)
			{
				Console.WriteLine($"[Inner Exception] {ex.InnerException.Message}");
			}
			return 0;
		}
	}
	//Get digimon by ID
	public static async Task<Digimon?> GetDigimonById(int Id)
	{
		try
		{
			int maxId = await GetMaxIdAsync();
			if (Id <= 0 || Id > maxId)
			{
				throw new ArgumentOutOfRangeException(nameof(Id), $"Invalid ID. Argument must be an integer between 1 and {maxId}.");
			}

			// Pontual instance for the DB connection
			using var context = new AppDbContext();
			return await context.Digimons
						.Include(d => d.Tier)
						.Include(d => d.Type)
						.Include(d => d.Attribute)
						.Include(d => d.SpecialMoves)
						.FirstOrDefaultAsync(d => d.Id == Id);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Digimon Database: Error while retrieving Digimon: Id = {Id}. " + ex.Message);
			return null;
		}
	}
	//Get Digimons By Attribute
	public static async Task<List<Digimon>?> GetDigimonsByAttributeId(int attId)
	{
		try
		{
			using var context = new AppDbContext();
			return await context.Digimons
				.Include(d => d.Tier)
				.Include(d => d.Type)
				.Include(d => d.Attribute)
				.Include(d => d.SpecialMoves)
				.Where(d => d.AttributeId == attId)
				.ToListAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Digimon Database: Error while retrieving Digimon list by attribute id: Id = {attId}. " + ex.Message);
			return null;
		}
	}
	//Get digimon by Tier
	public static async Task<List<Digimon>?> GetDigimonsByTierId(int tierId)
	{
		try
		{
			using var context = new AppDbContext();
			return await context.Digimons
				.Include(d => d.Tier)
				.Include(d => d.Type)
				.Include(d => d.Attribute)
				.Include(d => d.SpecialMoves)
				.Where(d => d.TierId == tierId)
				.ToListAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Digimon Database: Error while retrieving Digimon list by attribute id: Id = {tierId}. " + ex.Message);
			return null;
		}
	}
}