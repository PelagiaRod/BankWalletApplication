using BankingWallet.Domain.Entities;

namespace BankingWallet.Application.Interfaces;

public interface IWalletRepository
{
    Wallet? GetById(Guid walletId);

    void Update(Wallet wallet);
    void AddTransaction(Transaction transaction);
}