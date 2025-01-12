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
	//Empty constructo is necessary
	public AppDbContext() { }
	public AppDbContext(DbContextOptions<AppDbContext> options)
	  : base(options)
	{
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Digimon>()
	   .HasIndex(d => d.ImgUrl)
	   .IsUnique();
		// Model creating relation config
		modelBuilder.Entity<Digimon>()
		.HasMany(d => d.SpecialMoves)
		.WithMany(sm => sm.Digimons)
		.UsingEntity<Dictionary<string, object>>(
		"DigimonSpecialMove", 
		dsm => dsm.HasOne<SpecialMove>().WithMany().HasForeignKey("SpecialMoveId"),
		dsm => dsm.HasOne<Digimon>().WithMany().HasForeignKey("DigimonId")
	);
		modelBuilder.Entity<Attribute>()
			.HasOne(a => a.WeakAgainst)
			.WithMany()
			.HasForeignKey(a => a.WeakId)
			.OnDelete(DeleteBehavior.Restrict);

		modelBuilder.Entity<Attribute>()
			.HasOne(a => a.StrongAgainst)
			.WithMany()
			.HasForeignKey(a => a.StrongId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}