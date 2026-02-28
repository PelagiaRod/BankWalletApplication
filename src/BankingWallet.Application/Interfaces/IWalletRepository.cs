using BankingWallet.Domain.Entities;

namespace BankingWallet.Application.Interfaces;

public interface IWalletRepository
{
    Wallet? GetById(Guid walletId);

    Task UpdateAsync(Wallet wallet);
    Task AddTransactionAsync(Transaction transaction);
}