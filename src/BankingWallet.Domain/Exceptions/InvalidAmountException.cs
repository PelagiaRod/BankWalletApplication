namespace BankingWallet.Domain.Exceptions;

public class InvalidAmountException : DomainException
{
    public InvalidAmountException()
        : base("The amount specified is invalid.")
    {
    }
}
