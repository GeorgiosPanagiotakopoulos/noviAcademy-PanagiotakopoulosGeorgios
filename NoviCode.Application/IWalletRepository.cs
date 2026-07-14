namespace NoviCode;

public interface IWalletRepository
{
	Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default);
	Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<Wallet>> GetByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken = default);

	// Persist changes tracked on entities fetched via GetByIdAsync (e.g. after a deposit).
	Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
