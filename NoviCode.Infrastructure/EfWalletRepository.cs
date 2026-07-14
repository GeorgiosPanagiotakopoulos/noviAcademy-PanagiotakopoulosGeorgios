using Microsoft.EntityFrameworkCore;

namespace NoviCode;

public class EfWalletRepository : IWalletRepository
{
	private readonly AppDbContext _db;

	public EfWalletRepository(AppDbContext db) => _db = db;

	public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
	{
		await _db.Wallets.AddAsync(wallet, cancellationToken);
		await _db.SaveChangesAsync(cancellationToken);
	}

	// Tracked: a fetched wallet may be mutated (deposit/withdraw) and then persisted
	// with SaveChangesAsync by the caller.
	public Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
		_db.Wallets.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

	public async Task<IReadOnlyList<Wallet>> GetByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default) =>
		await _db.Wallets.Where(w => w.PlayerId == playerId).ToListAsync(cancellationToken);

	// Read-only leaderboard query — no tracking needed.
	public async Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken = default) =>
		await _db.Wallets.AsNoTracking().ToListAsync(cancellationToken);

	public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
		_db.SaveChangesAsync(cancellationToken);
}
