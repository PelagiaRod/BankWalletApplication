using BankingWallet.Domain.Common;

namespace BankingWallet.Domain.Wallet.Exceptions;

public class InsufficientFundsException : DomainException
{
    public InsufficientFundsException()
        : base("Insufficient funds for this operation.")
    {
    }
}
