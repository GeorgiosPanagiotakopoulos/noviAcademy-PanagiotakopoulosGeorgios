// WorldRank.Infrastructure/Persistence/WorldRankDbContext.cs
using Microsoft.EntityFrameworkCore;
using WorldRank.Domain.Entities;

namespace WorldRank.Infrastructure.Persistence;

public class WorldRankDbContext : DbContext
{
    // Ο constructor δέχεται τα options μέσω DI (τα δίνει είτε το AddDbContext,
    // είτε η design-time factory του Βήματος 4).
    public WorldRankDbContext(DbContextOptions<WorldRankDbContext> options)
        : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();
    public DbSet<Wallet> Wallets => Set<Wallet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>(player =>
        {
            player.ToTable("Players");
            player.HasKey(p => p.Id);
            player.Property(p => p.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Wallet>(wallet =>
        {
            wallet.ToTable("Wallets");
            wallet.HasKey(w => w.Id);

            // decimal ΧΩΡΙΣ precision βγάζει warning για πιθανό truncation — το ορίζουμε ρητά.
            wallet.Property(w => w.Balance).HasPrecision(18, 2);

            // Αποθηκεύουμε το Currency σαν κείμενο ("EUR") αντί για int — πιο ευανάγνωστο στο cross-check.
            wallet.Property(w => w.Currency).HasConversion<string>().HasMaxLength(3);

            // Ένα wallet ανά (Player, Currency) — αντικατοπτρίζει τον κανόνα του DuplicateWalletException.
            wallet.HasIndex(w => new { w.PlayerId, w.Currency }).IsUnique();

            // FK προς Player. Τα entities σου ΔΕΝ έχουν navigation properties,
            // οπότε το δηλώνουμε ρητά (χωρίς navigation και στις δύο μεριές).
            wallet.HasOne<Player>()
                  .WithMany()
                  .HasForeignKey(w => w.PlayerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}