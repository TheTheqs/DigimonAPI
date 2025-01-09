using Microsoft.EntityFrameworkCore;
//This is the Entity FrameWork main class, responsable for build the database bank, wich were Code First in this project

namespace DigimonAPI.entities;

public class AppDbContext : DbContext
{
	public DbSet<Digimon> Digimons { get; set; }
	public DbSet<Attribute> Attributes { get; set; }
	public DbSet<Tier> Tiers { get; set; }
	public DbSet<Type> Types { get; set; }
	public DbSet<SpecialMove> SpecialMoves { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Database=digimon_db;Username=postgres;Password=Marina22");
	}

	public AppDbContext(DbContextOptions<AppDbContext> options)
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
	  : base(options)
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
	{
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Model creating relation config
		modelBuilder.Entity<Attribute>()
			.HasOne(a => a.WeakAgainst)
			.WithMany()
			.HasForeignKey(a => a.WeakId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Attribute>()
			.HasOne(a => a.StrongAgainst)
			.WithMany()
			.HasForeignKey(a => a.StringId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}