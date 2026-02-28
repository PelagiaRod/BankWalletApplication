namespace BankingWallet.Domain.Exceptions;

public sealed class CurrencyMismatchException : DomainException
{
    public CurrencyMismatchException() 
        : base("Cannot operate on Money with different currencies.")
    {
    }
}
