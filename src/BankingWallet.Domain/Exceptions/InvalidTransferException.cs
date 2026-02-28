namespace BankingWallet.Domain.Exceptions;

public class InvalidTransferException : DomainException
{
    public InvalidTransferException()
        : base("The specified transfer is invalid.")
    {
    }
}
