using BankingWallet.Domain.Common;
using BankingWallet.Domain.Wallet.Exceptions;
using BankingWallet.Domain.Wallet.ValueObjects;

namespace BankingWallet.Domain.Wallet.Entities;

public abstract class Wallet : Entity
{
    public Money? Balance { get; private set; }
    public List<Transaction> Transactions { get; private set; } = new List<Transaction>();
    public Guid UserId { get; private set; }

    protected Wallet(Guid id, Money initialBalance)
    : base(id)
    {
        Balance = initialBalance;
    }

    public void Deposit(Money amount)
    {
        if (Balance == null)
        {
            Balance = amount;
        }
        else
        {
            Balance = Balance.Add(amount);
        }

        Transactions.Add(new Transaction(Guid.NewGuid(), Id, amount, TransactionType.Deposit, DateTime.UtcNow));
    }

    public void Withdraw(Money amount)
    {
        if (Balance == null)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        var newBalance = Balance.Subtract(amount);

        if (newBalance.Amount < 0)
        {
            throw new InsufficientFundsException();
        }

        Balance = newBalance;
        Transactions.Add(new Transaction(Guid.NewGuid(), Id, amount, TransactionType.Withdraw, DateTime.UtcNow));
    }

    public void SetBalance(Money amount)
    {
        Balance = amount;
    }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }
}


public sealed class FiatWallet : Wallet
{
    public FiatWallet() : base(Guid.NewGuid(), new Money(0, Currency.USD))
    {
    }

    public FiatWallet(Guid id, Money initialBalance)
        : base(id, initialBalance)
    {
    }
}

public sealed class CryptoWallet : Wallet
{
    public CryptoWallet() : base(Guid.NewGuid(), new Money(0, Currency.BTC))
    {
    }

    public CryptoWallet(Guid id, Money initialBalance)
        : base(id, initialBalance)
    {
    }
}
