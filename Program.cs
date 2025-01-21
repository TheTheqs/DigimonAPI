using DigimonAPI.entities;
using DigimonAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

//framework dependencie
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//Automation call (general)
builder.Services.AddHostedService<AutoTask>(); //Automation command
//build starter
var app = builder.Build();

//JSON formating
var options = new JsonSerializerOptions { WriteIndented = true };
//standart endpoint
app.MapGet("/", () => "Hello, World!");

//Get Digimon by ID
app.MapGet("/digimon/{Id}", async (string Id) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int DigimonId))
		{
			return Results.BadRequest($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid argument provided!");
		}

		// Retrieving from database
		Digimon? jsoned = await DDB.GetDigimonById(DigimonId);
		if (jsoned == null)
		{
			int maxId = await DDB.GetMaxIdAsync();
			return Results.NotFound($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid ID provided! Make sure the provided ID is an Integer between 1 and {maxId}.");
		}

		return Results.Json(DF.FormatDigimon(jsoned), options);
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Digimon: {err.Message}");
		return Results.StatusCode(500);
	}
});
app.MapGet("/digimon/attribute/{Id}", async (string Id) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int attId))
		{
			return Results.BadRequest($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid argument provided!");
		}

		// Retrieving from database
		List<Digimon>? jsoned = await DDB.GetDigimonsByAttributeId(attId);
		if (jsoned == null)
		{
			int maxId = await DDB.GetMaxIdAsync();
			return Results.NotFound($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid ID provided! Make sure the provided ID is an Integer between 1 and {maxId}.");
		}

		return Results.Json(DF.FormatList(jsoned), options);
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Digimon by atttribute: {err.Message}");
		return Results.StatusCode(500);
	}
});
app.MapGet("/digimon/tier/{Id}", async (string Id) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int tierId))
		{
			return Results.BadRequest($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid argument provided!");
		}

		// Retrieving from database
		List<Digimon>? jsoned = await DDB.GetDigimonsByTierId(tierId);
		if (jsoned == null)
		{
			int maxId = await DDB.GetMaxIdAsync();
			return Results.NotFound($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid ID provided! Make sure the provided ID is an Integer between 1 and {maxId}.");
		}

		return Results.Json(DF.FormatList(jsoned), options);
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Digimon by atttribute: {err.Message}");
		return Results.StatusCode(500);
	}
});
//Get special move by ID
app.MapGet("/move/{Id}", async (string Id) =>
{
	try
	{
		if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int sMoveId))
		{
			return Results.BadRequest($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid argument provided!");
		}

		// Retrieving from database
		SpecialMove? jsoned = await SDB.GetSpecialMoveById(sMoveId);
		if (jsoned == null)
		{
			int maxId = await SDB.GetMaxId();
			return Results.NotFound($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid ID provided! Make sure the provided ID is an Integer between 1 and {maxId}.");
		}

		return Results.Json(DF.FormatSpecialMove(jsoned), options);
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Special Move: {err.Message}");
		return Results.StatusCode(500);
	}
});
//Get Attribute
app.MapGet("/attribute/{Id}", async (string Id) =>
{
	try
	{
		if(!int.TryParse(Id, out int attId))
		{
			return Results.BadRequest("Invalid attribute ID providades");
		}
		if(attId == 0)
		{
			List<DigimonAPI.entities.Attribute>? aList = await ADB.GetAllAttributes();
			if (aList == null)
			{
				throw new Exception("Generated attributes is null!");
			}
			return Results.Json(DF.FormatAttributeList(aList));
		}
		DigimonAPI.entities.Attribute? att = await ADB.GetAttributeById(attId);
		if(att == null) return Results.BadRequest("Invalid attribute ID providades");
		return Results.Json(DF.FormatAttribute(att));
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Attribute: {err.Message}");
		return Results.StatusCode(500);
	}
});
//Get Tier
app.MapGet("/tier/{Id}", async (string Id) =>
{
	try
	{
		if (!int.TryParse(Id, out int tierId))
		{
			return Results.BadRequest("Invalid attribute ID providades");
		}
		if (tierId == 0)
		{
			List<Tier>? tList = await TDB.GetAllTiers();
			if (tList == null)
			{
				throw new Exception("Generated tiers are null!");
			}
			return Results.Json(DF.FormatList(tList));
		}
		Tier? tier = await TDB.GetTierById(tierId);
		if (tier == null) return Results.BadRequest("Invalid tier ID providades");
		return Results.Json(DF.FormatTier(tier));
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Tier: {err.Message}");
		return Results.StatusCode(500);
	}
});

//This endpoint was created to manually generate a new digimon in database
app.MapPost("/{Index}", async (string Index) =>
{
	if (string.IsNullOrWhiteSpace(Index) || !int.TryParse(Index, out int digimonIndex))
	{
		return Results.BadRequest($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid argument provided!");
	}
	try
	{
		Digimon? digimon = await DS.ParseDigimon(digimonIndex);
		if(digimon == null)
		{
			return Results.NotFound($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid Index provided! The provided index could not generate a valid Digimon. Index: {digimonIndex}");
		}
		digimon = await DDB.SaveDigimon(digimon);
		int nextDesc = await SDB.GetNextDescription();
		while (nextDesc != -1)
		{
			bool result = await BSD.GenerateSkillDescription(nextDesc);
			if (!result)
			{
				Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Error generating new special move description. Id {nextDesc}");
				break;
			}
			nextDesc = await SDB.GetNextDescription();
		}
		
		return Results.Json(DF.FormatDigimon(digimon), options);

	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Posting New Digimon: {err.Message}");
		return Results.StatusCode(500);
	}
});
app.Run();
