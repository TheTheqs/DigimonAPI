using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

//framework dependencie
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//Automation call (general)
//builder.Services.AddHostedService<AutoTask>(); //Automation command
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
			return Results.NotFound($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid ID provided! Make sure the provided ID is an Integer between 1 and 1173.");
		}

		return Results.Json(DF.FormatDigimon(jsoned), options);
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Digimon: {err.Message}");
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
			return Results.NotFound($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][System] Invalid ID provided! Make sure the provided ID is an Integer between 1 and 2010.");
		}

		return Results.Json(DF.FormatSpecialMove(jsoned), options);
	}
	catch (Exception err)
	{
		Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][ERROR] Get Special Move: {err.Message}");
		return Results.StatusCode(500);
	}
});
app.Run();
