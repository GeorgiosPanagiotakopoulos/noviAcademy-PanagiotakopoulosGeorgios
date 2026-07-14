namespace NoviCode;

public class InMemoryWalletRepository : IWalletRepository
{
	private readonly List<Wallet> _wallets = new();

	public Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
	{
		_wallets.Add(wallet);
		return Task.CompletedTask;
	}

	public Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
		Task.FromResult(_wallets.FirstOrDefault(w => w.Id == id));

	public Task<IReadOnlyList<Wallet>> GetByPlayerAsync(Guid playerId, CancellationToken cancellationToken = default) =>
		Task.FromResult<IReadOnlyList<Wallet>>(_wallets.Where(w => w.PlayerId == playerId).ToList());

	public Task<IReadOnlyList<Wallet>> GetAllAsync(CancellationToken cancellationToken = default) =>
		Task.FromResult<IReadOnlyList<Wallet>>(_wallets.ToList());

	// Mutations happen in-place on the tracked object, so there is nothing extra to persist.
	public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
}
