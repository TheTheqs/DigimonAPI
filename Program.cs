using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

//framework dependencie
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//build starter
var app = builder.Build();

//JSON formating
var options = new JsonSerializerOptions { WriteIndented = true };
//Favicon
//standart endpoint
app.MapGet("/", () => "Hello, World!");

app.MapGet("/{Id}", async (string Id) =>
{
	try
	{
		//Index validation
		if (string.IsNullOrWhiteSpace(Id) || !int.TryParse(Id, out int index))
		{
			return Results.NotFound("[System] Invalid ID provided!");
		}

		//Trying to get digimon from Digimon.net
		Digimon? digimon = await DDB.SaveDigimon(await DS.ParseDigimon(index));
		//Data Format
		object? jsoned = DF.FormatDigimon(digimon);
		if(jsoned != null)
		{
			return Results.Json(jsoned, options);
		}
		return Results.NotFound("No digimon found!");
	}
	catch(Exception err)
	{
		Console.WriteLine("[ERROR] Get Digimon:" +  err.Message);
		return Results.NotFound("[ERROR] Get Digimon: An error has occurred!");
	}
});

app.Run();
