using Microsoft.AspNetCore.Mvc;

namespace NoviCode.Api;

[ApiController]
[Route("players")]
public class PlayersController : ControllerBase
{
	private readonly IPlayerService _players;

	public PlayersController(IPlayerService players) => _players = players;

	// POST /players — create a player (the decorator writes through to the cache).
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreatePlayerRequest request, CancellationToken cancellationToken)
	{
		Player player;
		try
		{
			player = await _players.CreateAsync(request.Name, request.Score, cancellationToken);
		}
		catch (ArgumentException ex)
		{
			// Empty name / negative score → 400.
			return BadRequest(new { error = ex.Message });
		}

		return CreatedAtAction(nameof(GetById), new { id = player.Id }, PlayerResponse.From(player));
	}

	// GET /players/{id} — 200 or 404 (caching handled by the decorator).
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
	{
		var player = await _players.GetByIdAsync(id, cancellationToken);
		return player is null ? NotFound() : Ok(PlayerResponse.From(player));
	}

	// GET /players — list all players (cached).
	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var players = await _players.GetAllAsync(cancellationToken);
		return Ok(players.Select(PlayerResponse.From));
	}
}
