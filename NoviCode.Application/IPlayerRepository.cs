namespace NoviCode;

public interface IPlayerRepository
{
	Task AddAsync(Player player, CancellationToken cancellationToken = default);
	Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Player?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken = default);
}
