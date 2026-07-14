using NoviCode;

namespace NoviCode.Api;

// Request DTO — what the client sends to create a player.
public record CreatePlayerRequest(string Name, int Score);

// Response DTO — what the client receives. Never expose the domain entity directly.
public record PlayerResponse(Guid Id, string Name, int Score)
{
	public static PlayerResponse From(Player player) =>
		new(player.Id, player.Name, player.Score);
}
