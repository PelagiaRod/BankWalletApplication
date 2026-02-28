namespace BankingWallet.Domain.Exceptions;

public class InsufficientFundsException : DomainException
{
    public InsufficientFundsException()
        : base("Insufficient funds for this operation.")
    {
    }
}
