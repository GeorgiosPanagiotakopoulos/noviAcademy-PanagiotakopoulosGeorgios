using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WorldRank.Application.Interfaces;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Enums;
using WorldRank.Domain.Exceptions;
using WorldRank.Infrastructure.Persistence;

namespace WorldRank.Infrastructure.Repositories;

public class DBWalletRepository : IWalletRepository
{
    private readonly IDbContextFactory<WorldRankDbContext> _contextFactory;
    private readonly ILogger<DBWalletRepository> _logger;

    public DBWalletRepository(
        IDbContextFactory<WorldRankDbContext> contextFactory,
        ILogger<DBWalletRepository> logger)
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public void Add(Wallet wallet)
    {
        using var db = _contextFactory.CreateDbContext();
        var exists = db.Wallets.Any(item => item.PlayerId == wallet.PlayerId && item.Currency == wallet.Currency);
        if (exists)
            throw new DuplicateWalletException(wallet.PlayerId, wallet.Currency);

        var toAdd = new Wallet(0, wallet.PlayerId, wallet.Currency, wallet.Balance, wallet.IsBlocked);
        db.Wallets.Add(toAdd);
        db.SaveChanges();
        _logger.LogInformation("Wallet created for player {PlayerId} in {Currency} with balance {Balance}",
            toAdd.PlayerId, toAdd.Currency, toAdd.Balance);
    }

    public Wallet[] GetAll()
    {
        using var db = _contextFactory.CreateDbContext();
        return db.Wallets.AsNoTracking().ToArray();
    }

    public List<Wallet> GetAllWalletsByPlayerId(int playerId)
    {
        using var db = _contextFactory.CreateDbContext();
        return db.Wallets.AsNoTracking().Where(item => item.PlayerId == playerId).ToList();
    }

    public Wallet GetWallet(int playerId, Currency currency)
    {
        using var db = _contextFactory.CreateDbContext();
        var wallet = db.Wallets.AsNoTracking()
            .SingleOrDefault(item => item.PlayerId == playerId && item.Currency == currency);
        if (wallet is null)
            throw new WalletNotFoundException(playerId, currency);
        return wallet;
    }

    public void UpdateBalance(int playerId, Currency currency, decimal newBalance)
    {
        using var db = _contextFactory.CreateDbContext();
        var wallet = GetTracked(db, playerId, currency);
        wallet.SetBalance(newBalance);
        db.SaveChanges();
        _logger.LogInformation("Player {PlayerId} {Currency} wallet balance set to {Balance}",
            playerId, currency, newBalance);
    }

    public void Deposit(int playerId, Currency currency, decimal amount)
    {
        using var db = _contextFactory.CreateDbContext();
        var wallet = GetTracked(db, playerId, currency);
        wallet.Deposit(amount);
        db.SaveChanges();
        _logger.LogInformation("Deposited {Amount} to player {PlayerId} {Currency} wallet (balance {Balance})",
            amount, playerId, currency, wallet.Balance);
    }

    public void Withdraw(int playerId, Currency currency, decimal amount)
    {
        using var db = _contextFactory.CreateDbContext();
        var wallet = GetTracked(db, playerId, currency);
        wallet.Withdraw(amount);
        db.SaveChanges();
        _logger.LogInformation("Withdrew {Amount} from player {PlayerId} {Currency} wallet (balance {Balance})",
            amount, playerId, currency, wallet.Balance);
    }

    public void Block(int playerId, Currency currency)
    {
        using var db = _contextFactory.CreateDbContext();
        var wallet = GetTracked(db, playerId, currency);
        wallet.Block();
        db.SaveChanges();
        _logger.LogInformation("Player {PlayerId} {Currency} wallet blocked", playerId, currency);
    }

    public void Unblock(int playerId, Currency currency)
    {
        using var db = _contextFactory.CreateDbContext();
        var wallet = GetTracked(db, playerId, currency);
        wallet.Unblock();
        db.SaveChanges();
        _logger.LogInformation("Player {PlayerId} {Currency} wallet unblocked", playerId, currency);
    }

    private static Wallet GetTracked(WorldRankDbContext db, int playerId, Currency currency)
    {
        var wallet = db.Wallets.SingleOrDefault(item => item.PlayerId == playerId && item.Currency == currency);
        if (wallet is null)
            throw new WalletNotFoundException(playerId, currency);
        return wallet;
    }
}