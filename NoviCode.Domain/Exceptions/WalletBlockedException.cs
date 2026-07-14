namespace NoviCode;

// Raised when a fund operation is attempted on a blocked wallet.
public class WalletBlockedException : WalletException
{
	public Guid WalletId { get; }

	public WalletBlockedException(Guid walletId)
		: base($"Wallet {walletId} is blocked and cannot accept fund operations.")
	{
		WalletId = walletId;
	}
}
