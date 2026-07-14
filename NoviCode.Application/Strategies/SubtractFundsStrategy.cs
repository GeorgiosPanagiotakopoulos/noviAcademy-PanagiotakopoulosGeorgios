namespace NoviCode;

public class SubtractFundsStrategy : IFundsStrategy
{
	// Throws InsufficientFundsException if the balance is too low.
	public void Execute(Wallet wallet, decimal amount) => wallet.Withdraw(amount);
}
