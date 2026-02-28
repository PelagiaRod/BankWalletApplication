using BankingWallet.Domain.Entities;
using Xunit;
using BankingWallet.Domain.ValueObjects;
using BankingWallet.Domain.Exceptions;
using FluentAssertions;


namespace BankingWallet.Domain.Tests.Entities;

public class WalletTests
{
    [Fact]
    public void Deposit_Should_Increase_Balance()
    {
        // Arrange
        var wallet = new FiatWallet(Guid.NewGuid(), new Money(0, Currency.USD));
        var depositAmount = new Money(100, Currency.USD);

        // Act
        wallet.Deposit(depositAmount);

        // Assert
        wallet.Balance.Should().BeEquivalentTo(depositAmount);
    }

    [Fact]
    public void Withdraw_Should_Decrease_Balance()
    {
        // Arrange
        var wallet = new FiatWallet(Guid.NewGuid(), new Money(0, Currency.USD));
        var initialDeposit = new Money(200, Currency.USD);
        wallet.Deposit(initialDeposit);
        var withdrawAmount = new Money(50, Currency.USD);

        // Act
        wallet.Withdraw(withdrawAmount);

        // Assert
        var expectedBalance = new Money(150, Currency.USD);
        wallet.Balance.Should().BeEquivalentTo(expectedBalance);
    }

    [Fact]
    public void Withdraw_Should_Throw_Exception_When_Insufficient_Funds()
    {
        // Arrange
        var wallet = new FiatWallet(Guid.NewGuid(), new Money(0, Currency.USD));
        var withdrawAmount = new Money(100, Currency.USD);

        // Act & Assert
        Assert.Throws<InsufficientFundsException>(() => wallet.Withdraw(withdrawAmount));
    }
}
