namespace NoviCode;

public class InvalidAmountException : WalletException
{
	public decimal Amount { get; }

	public InvalidAmountException(decimal amount)
		: base($"Amount must be positive. Provided: {amount:0.00}.")
	{
		Amount = amount;
	}
}
