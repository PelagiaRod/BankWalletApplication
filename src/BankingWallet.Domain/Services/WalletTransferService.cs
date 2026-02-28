using BankingWallet.Domain.Entities;
using BankingWallet.Domain.ValueObjects;
using BankingWallet.Domain.Exceptions;

namespace BankingWallet.Domain.Services;

public sealed class WalletTransferService
{
    public void Transfer(Wallet from, Wallet to, Money amount)
    {
        if (from.Id == to.Id)
            throw new InvalidTransferException();

        if (from.Balance == null || to.Balance == null)
            throw new InvalidTransferException();

        from.Withdraw(amount);
        to.Deposit(amount);
    }
}