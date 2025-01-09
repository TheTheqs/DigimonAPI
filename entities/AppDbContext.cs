using Microsoft.EntityFrameworkCore;

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
	  : base(options)
	{
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configurações de relacionamentos adicionais (exemplo Weak/Strong)
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