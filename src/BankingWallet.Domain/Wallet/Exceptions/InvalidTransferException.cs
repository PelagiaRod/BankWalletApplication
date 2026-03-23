using BankingWallet.Domain.Common;

namespace BankingWallet.Domain.Wallet.Exceptions;

public class InvalidTransferException : DomainException
{
    public InvalidTransferException()
        : base("The specified transfer is invalid.")
    {
    }
}
