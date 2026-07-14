using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;
using WorldRank.Infrastructure.Persistence;

namespace WorldRank.Infrastructure.Repositories;

public class DBPlayerRepository : IPlayerRepository
{
    private readonly IDbContextFactory<WorldRankDbContext> _contextFactory;
    private readonly ILogger<DBPlayerRepository> _logger;

    public DBPlayerRepository(
        IDbContextFactory<WorldRankDbContext> contextFactory,
        ILogger<DBPlayerRepository> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public void AddPlayer(Player player)
    {
        using var db = _contextFactory.CreateDbContext();

        db.Players.Add(player);

        db.SaveChanges();

        _logger.LogInformation("Player {PlayerId} ({Name}) added with score {Score}",
            player.Id, player.Name, player.Score);
    }

    public IEnumerable<Player> GetAllPlayers()
    {
        using var db = _contextFactory.CreateDbContext();
        return db.Players.AsNoTracking().ToList();
    }

    public void DeletePlayer(int playerId)
    {
        using var db = _contextFactory.CreateDbContext();
        var player = db.Players.FirstOrDefault(item => item.Id == playerId);
        if (player is null)
        {
            _logger.LogWarning("Delete skipped: player {PlayerId} not found", playerId);
            return;
        }
        db.Players.Remove(player);
        db.SaveChanges();
        _logger.LogInformation("Player {PlayerId} deleted", playerId);
    }

    public Player? FindPlayer(int playerId)
    {
        using var db = _contextFactory.CreateDbContext();
        return db.Players.AsNoTracking().FirstOrDefault(item => item.Id == playerId);
    }

    public IEnumerable<IGrouping<int, Player>> GroupPlayersByScore()
    {
        using var db = _contextFactory.CreateDbContext();
        return db.Players
            .AsNoTracking()
            .ToList()
            .GroupBy(player => player.Score)
            .OrderByDescending(group => group.Key);
    }
}