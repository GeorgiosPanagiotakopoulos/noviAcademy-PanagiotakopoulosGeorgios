namespace WorldRank;

public class Player
{
	public Guid Id { get; }
	public string Name { get; }
	public int Score { get; private set; }

	public Player(string name)
	{
		if (string.IsNullOrEmpty(name))
			throw new ArgumentException("Name cannot be null or empty.", nameof(name));

		Id = Guid.NewGuid();
		Name = name;
	}

	public void UpdateScore(int newScore)
	{
		if (newScore < 0)
			throw new ArgumentOutOfRangeException(nameof(newScore), "Score cannot be negative.");

		Score = newScore;
	}

	public override string ToString() =>
			$"[{Id}] {Name} - Score: {Score}";
}
