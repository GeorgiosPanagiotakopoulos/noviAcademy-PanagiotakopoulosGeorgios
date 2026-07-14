using NoviCode;

namespace NoviCode.Api;

// Request DTOs — the shape the client sends. Kept separate from the domain entity.
public record CreateWalletRequest(Guid PlayerId, Currency Currency);

public record DepositRequest(decimal Amount);

// Response DTO — the shape the client receives. Never expose the domain entity directly.
public record WalletResponse(Guid Id, Guid PlayerId, string Currency, decimal Balance, bool IsBlocked)
{
	public static WalletResponse From(Wallet wallet) =>
		new(wallet.Id, wallet.PlayerId, wallet.Currency.ToString(), wallet.Balance, wallet.IsBlocked);
}
