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

app.MapGet("/test", async () =>
{
	try
	{
		String webPage = "https://digimon.net/reference_en/detail.php?directory_name=ryugumon";
		String? providadedHTML = await HP.GetHTML(webPage);
		if (providadedHTML == null)
		{
			return Results.NotFound("HTML is null!");
		} 
		else
		{
			if(TC.GenerateTxt(providadedHTML))
			{
				return Results.Ok("TXT was successfully generated!");
			}
			else
			{
				return Results.BadRequest("Failed to generate TXT");
			}
		}
	}
	catch(Exception err)
	{
		Console.WriteLine(err.Message);
		return Results.NotFound("An error has occurred!");
	}
});

app.Run();
