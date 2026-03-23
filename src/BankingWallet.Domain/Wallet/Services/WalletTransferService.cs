using BankingWallet.Domain.Wallet.Entities;
using BankingWallet.Domain.Wallet.ValueObjects;
using BankingWallet.Domain.Wallet.Exceptions;
using WalletObj = BankingWallet.Domain.Wallet.Entities.Wallet;

namespace BankingWallet.Domain.Wallet.Services;

public sealed class WalletTransferService
{
    public void Transfer(WalletObj from, WalletObj to, Money amount)
    {
        if (from.Id == to.Id)
            throw new InvalidTransferException();

        if (from.Balance == null || to.Balance == null)
            throw new InvalidTransferException();

        if (from.Balance.Amount < amount.Amount)
            throw new InsufficientFundsException();

        from.Withdraw(amount);
        to.Deposit(amount);
    }
}