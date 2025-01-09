using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//framework dependencie
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build(); //build starter

app.MapGet("/", () => "Hello, World!"); //standart endpoint

app.Run();
