namespace NoviCode;

// Base type for every wallet-related domain exception.
public abstract class WalletException : Exception
{
	protected WalletException(string message) : base(message)
	{
	}
}
