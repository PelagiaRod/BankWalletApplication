using BankingWallet.Domain.Exceptions;

namespace BankingWallet.Domain.ValueObjects;

public class Money
{
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }

    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (other.Currency != Currency)
            throw new CurrencyMismatchException();

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (other.Currency != Currency)
            throw new CurrencyMismatchException();

        return new Money(Amount - other.Amount, Currency);
    }
}
