using WorldRank.Domain.Wallets;

namespace WorldRank.Application.Strategies
{
    public class ForceSubstractFundsStrategy : IFundsStrategy
    {
        public FundsOperation Operation => FundsOperation.ForceSubtract;

        public void Execute(Wallet wallet, decimal amount) => wallet.ForceWithdraw(amount);
    }
}