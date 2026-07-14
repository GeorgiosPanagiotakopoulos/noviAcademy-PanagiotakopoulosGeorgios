namespace NoviCode;

public class AddFundsStrategy : IFundsStrategy
{
	public void Execute(Wallet wallet, decimal amount) => wallet.Deposit(amount);
}
