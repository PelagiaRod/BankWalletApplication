using BankingWallet.Domain.Wallet.Entities;
using WalletObj = BankingWallet.Domain.Wallet.Entities.Wallet;

namespace BankingWallet.Application.Wallet.Interfaces;

public interface IWalletRepository
{
    WalletObj? GetById(Guid walletId);

    Task UpdateAsync(WalletObj wallet);
    Task AddTransactionAsync(Transaction transaction);
}