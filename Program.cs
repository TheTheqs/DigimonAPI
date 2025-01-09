using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//framework dependencie
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//build starter
var app = builder.Build();

//JSON formating
var options = new JsonSerializerOptions { WriteIndented = true };

//standart endpoint
app.MapGet("/", () => "Hello, World!");

app.MapGet("/test", () =>
{
	try
	{
		ICollection<SpecialMove> specialMoves = new HashSet<SpecialMove>();
		specialMoves.Add(new SpecialMove("Final Excalibur"));
		specialMoves.Add(new SpecialMove("Zero Heavens"));
		specialMoves.Add(new SpecialMove("Devotion Field"));
		Digimon testDigimon = new Digimon("Dominimon", "Desc", "imgURL", new Tier("Mega"), new DigimonAPI.entities.Type("Dominion"), new DigimonAPI.entities.Attribute("Vaccine"), specialMoves);
		if(testDigimon != null)
		{
			return Results.Json(testDigimon, options); //Results
		} 
		else
		{
			return Results.NotFound("The related digimon does not exist in the database");
		}
	}
	catch(Exception err)
	{
		Console.WriteLine(err.Message);
		return Results.NotFound("An error has occurred!");
	}
});

app.Run();
