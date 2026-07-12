using WorldRank.Domain.Enums;
using WorldRank.Domain.Wallets;

namespace WorldRank.Application.Repositories
{
	public interface IWalletRepository
	{
		void Add(Wallet wallet);

		List<Wallet> GetAllWalletsByPlayerId(int playerId);
        Wallet GetWallet(int playerId, Currency currency);

        void UpdateBalance(int playerId, Currency currency, decimal newBalance);

		void Deposit(int playerId, Currency currency, decimal amount);

        void Withdraw(int playerId, Currency currency, decimal amount);

        void ForceWithdraw(int playerId, Currency currency, decimal amount);

        void Block(int playerId, Currency currency);

		void Unblock(int playerId, Currency currency);
	}
}
