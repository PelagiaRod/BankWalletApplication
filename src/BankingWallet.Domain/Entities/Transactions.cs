using BankingWallet.Domain.Common;
using BankingWallet.Domain.Exceptions;
using BankingWallet.Domain.ValueObjects;

namespace BankingWallet.Domain.Entities;

public enum TransactionType
{
    Deposit,
    Withdraw,
    Transfer
}

public sealed class Transaction : Entity
{
    public Guid WalletId { get; private set; }
    public Money? Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime OccurredAt { get; private set; }

    public Transaction() : base(Guid.NewGuid())
    {
    }

    public Transaction(Guid id, Guid walletId, Money amount, TransactionType type, DateTime occurredAt) : base(id)
    {
        if (amount.Amount <= 0)
            throw new InvalidAmountException();

        WalletId = walletId;
        Amount = amount;
        Type = type;
        OccurredAt = occurredAt;
    }
}