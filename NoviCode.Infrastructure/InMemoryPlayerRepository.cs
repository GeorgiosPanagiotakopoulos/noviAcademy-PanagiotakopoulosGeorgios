namespace NoviCode;

public class InMemoryPlayerRepository : IPlayerRepository
{
	private readonly List<Player> _players = new();

	public Task AddAsync(Player player, CancellationToken cancellationToken = default)
	{
		_players.Add(player);
		return Task.CompletedTask;
	}

	public Task<Player?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
		Task.FromResult(_players.FirstOrDefault(p => p.Id == id));

	public Task<Player?> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
		Task.FromResult(_players.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));

	public Task<IReadOnlyList<Player>> GetAllAsync(CancellationToken cancellationToken = default) =>
		Task.FromResult<IReadOnlyList<Player>>(_players.ToList());
}
