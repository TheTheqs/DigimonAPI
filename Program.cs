using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//injeção do framework do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello, World!");

app.Run();
