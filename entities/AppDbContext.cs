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
	public DbSet<Usable> Usables { get; set; }
	public DbSet<Artifact> Artifacts { get; set; }
	public DbSet<Event> Events { get; set; }
	public DbSet<SpawnArtifact> SpawnArtifacts { get; set; }
	public DbSet<SpawnUsable> SpawnUsables { get; set; }
	public DbSet<SpawnDigimon> SpawnDigimons { get; set; }
	public DbSet<SpawnEvent> SpawnEvents { get; set; }


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
		//Digimon SpecialMove n:n relation
		modelBuilder.Entity<Digimon>()
			.HasMany(d => d.SpecialMoves)
			.WithMany(sm => sm.Digimons)
			.UsingEntity<Dictionary<string, object>>(
			"DigimonSpecialMove", 
			dsm => dsm.HasOne<SpecialMove>().WithMany().HasForeignKey("SpecialMoveId"),
			dsm => dsm.HasOne<Digimon>().WithMany().HasForeignKey("DigimonId"));
		
		//Attribute Attribute Weak 1:n relation
		modelBuilder.Entity<Attribute>()
			.HasOne(a => a.WeakAgainst)
			.WithMany()
			.HasForeignKey(a => a.WeakId)
			.OnDelete(DeleteBehavior.Restrict);

		//Attribute Attribute Strong 1:n relation
		modelBuilder.Entity<Attribute>()
			.HasOne(a => a.StrongAgainst)
			.WithMany()
			.HasForeignKey(a => a.StrongId)
			.OnDelete(DeleteBehavior.Restrict);

		//Digimon SpawnDigimon n:n relation
		modelBuilder.Entity<SpawnDigimon>()
			.HasMany(d => d.Digimons)
			.WithMany(sd => sd.Spawns)
			.UsingEntity<Dictionary<string, object>>(
			"DigimonSpawn",
			dsd => dsd.HasOne<Digimon>().WithMany().HasForeignKey("DigimonId"),
			dsd => dsd.HasOne<SpawnDigimon>().WithMany().HasForeignKey("SpawnDigimonId"));

		//Artifact SpawnArtifact n:n relation
		modelBuilder.Entity<SpawnArtifact>()
			.HasMany(a => a.Artifacts)
			.WithMany(sa => sa.Spawns)
			.UsingEntity<Dictionary<string, object>>(
			"ArtifactSpawn",
			asa => asa.HasOne<Artifact>().WithMany().HasForeignKey("ArtifactId"),
			asa => asa.HasOne<SpawnArtifact>().WithMany().HasForeignKey("SpawnArtifactId"));

		//Usable SpawnUsable n:n relation
		modelBuilder.Entity<SpawnUsable>()
			.HasMany(u => u.Usables)
			.WithMany(sd => sd.Spawns)
			.UsingEntity<Dictionary<string, object>>(
			"UsableSpawn",
			usu => usu.HasOne<Usable>().WithMany().HasForeignKey("UsableId"),
			usu => usu.HasOne<SpawnUsable>().WithMany().HasForeignKey("SpawnUsableId"));

		//Event SpawnEvent n:n relation
		modelBuilder.Entity<SpawnEvent>()
			.HasMany(e => e.Events)
			.WithMany(sd => sd.Spawns)
			.UsingEntity<Dictionary<string, object>>(
			"EventSpawn",
			ese => ese.HasOne<Event>().WithMany().HasForeignKey("EventId"),
			ese => ese.HasOne<SpawnEvent>().WithMany().HasForeignKey("SpawnEventId"));

		//Artifact ContentArtifact n:n relation
		modelBuilder.Entity<SpawnArtifact>()
			.HasMany(sa => sa.Contents)
			.WithMany(ca => ca.ArtifactsSpawns)
			.UsingEntity<Dictionary<string, object>>(
			"ArtifactsContents",
			saca => saca.HasOne<ContentArtifacts>().WithMany().HasForeignKey("ContentId"),
			saca => saca.HasOne<SpawnArtifact>().WithMany().HasForeignKey("SpawnId"));

		//Digimon ContentDigimon n:n relation
		modelBuilder.Entity<SpawnDigimon>()
			.HasMany(sd => sd.Contents)
			.WithMany(cd => cd.DigimonsSpawns)
			.UsingEntity<Dictionary<string, object>>(
			"DigimonsContents",
			saca => saca.HasOne<ContentDigimons>().WithMany().HasForeignKey("ContentId"),
			saca => saca.HasOne<SpawnDigimon>().WithMany().HasForeignKey("SpawnId"));

		//Usable ContentUsable n:n relation
		modelBuilder.Entity<SpawnUsable>()
			.HasMany(ss => ss.Contents)
			.WithMany(cs => cs.UsablesSpawns)
			.UsingEntity<Dictionary<string, object>>(
			"UsablesContents",
			saca => saca.HasOne<ContentUsables>().WithMany().HasForeignKey("ContentId"),
			saca => saca.HasOne<SpawnUsable>().WithMany().HasForeignKey("SpawnId"));

		//Event ContentEvent n:n relation
		modelBuilder.Entity<SpawnEvent>()
			.HasMany(se => se.Contents)
			.WithMany(ce => ce.EventsSpawns)
			.UsingEntity<Dictionary<string, object>>(
			"EventsContents",
			saca => saca.HasOne<ContentEvents>().WithMany().HasForeignKey("ContentId"),
			saca => saca.HasOne<SpawnEvent>().WithMany().HasForeignKey("SpawnId"));

		// Dungeon -> ContentUsables (1:n relation)
		modelBuilder.Entity<Dungeon>()
			.HasOne(d => d.Usables)
			.WithMany(cu => cu.Dungeons)
			.HasForeignKey(d => d.UsablesContentId);

		// Dungeon -> ContentEvents (1:n relation)
		modelBuilder.Entity<Dungeon>()
			.HasOne(d => d.Events)
			.WithMany(ce => ce.Dungeons)
			.HasForeignKey(d => d.EventsContentId);

		// Dungeon -> ContentArtifacts (1:n relation)
		modelBuilder.Entity<Dungeon>()
			.HasOne(d => d.Artifacts)
			.WithMany(ca => ca.Dungeons)
			.HasForeignKey(d => d.ArtifactsContentId);

		// Dungeon -> ContentDigimon (1:n relation)
		modelBuilder.Entity<Dungeon>()
			.HasOne(d => d.Digimons)
			.WithMany(cd => cd.Dungeons)
			.HasForeignKey(d => d.DigimonsContentId);

	}
}