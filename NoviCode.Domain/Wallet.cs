namespace NoviCode;

public class Wallet
{
	public Guid Id { get; }
	public Guid PlayerId { get; }
	public Currency Currency { get; }
	public decimal Balance { get; private set; }
	public bool IsBlocked { get; private set; }

	public Wallet(Guid playerId, Currency currency)
	{
		Id = Guid.NewGuid();
		PlayerId = playerId;
		Currency = currency;
	}

	// Parameterless ctor used only by EF Core to materialise rows (properties set via backing fields).
	private Wallet()
	{
	}

	// Balance is encapsulated: it changes only through Deposit / Withdraw — never set from outside.
	public void Deposit(decimal amount)
	{
		if (IsBlocked)
			throw new WalletBlockedException(Id);

		if (amount <= 0)
			throw new InvalidAmountException(amount);

		Balance += amount;
	}

	public void Withdraw(decimal amount)
	{
		if (IsBlocked)
			throw new WalletBlockedException(Id);

		if (amount <= 0)
			throw new InvalidAmountException(amount);

		if (amount > Balance)
			throw new InsufficientFundsException(amount, Balance);

		Balance -= amount;
	}

	// Subtracts even if the result is negative (used by ForceSubtractFundsStrategy).
	public void ForceWithdraw(decimal amount)
	{
		if (amount <= 0)
			throw new InvalidAmountException(amount);

		Balance -= amount;
	}

	public void Block() => IsBlocked = true;
	public void Unblock() => IsBlocked = false;

	public override string ToString() =>
		$"[{Id}] {Currency} {Balance:0.00}{(IsBlocked ? " (blocked)" : string.Empty)}";
}
