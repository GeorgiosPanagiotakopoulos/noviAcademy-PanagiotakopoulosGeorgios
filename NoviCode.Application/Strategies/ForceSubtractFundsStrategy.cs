namespace NoviCode;

public class ForceSubtractFundsStrategy : IFundsStrategy
{
	// Subtracts even if the result is a negative balance.
	public void Execute(Wallet wallet, decimal amount) => wallet.ForceWithdraw(amount);
}
