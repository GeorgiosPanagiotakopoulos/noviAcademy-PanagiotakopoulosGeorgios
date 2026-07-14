using Microsoft.EntityFrameworkCore;

namespace NoviCode;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Player> Players => Set<Player>();
	public DbSet<Wallet> Wallets => Set<Wallet>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Player>(entity =>
		{
			entity.ToTable("Players");
			entity.HasKey(p => p.Id);
			entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
			entity.Property(p => p.Score);
		});

		modelBuilder.Entity<Wallet>(entity =>
		{
			entity.ToTable("Wallets");
			entity.HasKey(w => w.Id);
			entity.Property(w => w.PlayerId);
			entity.Property(w => w.Currency).HasConversion<string>().HasMaxLength(3);
			entity.Property(w => w.Balance).HasPrecision(18, 2);
			entity.Property(w => w.IsBlocked);
		});
	}
}
