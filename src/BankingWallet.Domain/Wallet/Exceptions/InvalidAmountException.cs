using BankingWallet.Domain.Common;

namespace BankingWallet.Domain.Wallet.Exceptions;

public class InvalidAmountException : DomainException
{
    public InvalidAmountException()
        : base("The amount specified is invalid.")
    {
    }
}
