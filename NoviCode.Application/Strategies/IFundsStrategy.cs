namespace NoviCode;

// Strategy pattern: a family of fund operations behind one interface.
// The caller picks a behaviour by passing the right implementation — no if/switch.
public interface IFundsStrategy
{
	void Execute(Wallet wallet, decimal amount);
}
