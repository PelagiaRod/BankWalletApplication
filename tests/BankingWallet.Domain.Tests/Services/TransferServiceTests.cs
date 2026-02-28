using BankingWallet.Domain.Entities;
using BankingWallet.Domain.Exceptions;
using BankingWallet.Domain.Services;
using BankingWallet.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BankingWallet.Domain.Tests.Services;

public class TransferServiceTests
{
    [Fact]
    public void Transfer_Should_Move_Money_Between_Wallets()
    {
        var fromWallet = new FiatWallet(Guid.NewGuid(), new Money(200, Currency.USD));
        var toWallet = new FiatWallet(Guid.NewGuid(), new Money(200, Currency.USD));

        var transferService = new WalletTransferService();

        transferService.Transfer(fromWallet, toWallet, new Money(30, Currency.USD));

        fromWallet.Balance.Should().BeEquivalentTo(new Money(170, Currency.USD));
        toWallet.Balance.Should().BeEquivalentTo(new Money(230, Currency.USD));
    }

    [Fact]
    public void Transfer_Should_Fail_When_Source_Has_Insufficient_Funds()
    {
        var fromWallet = new FiatWallet(Guid.NewGuid(), new Money(10, Currency.USD));
        var toWallet = new FiatWallet(Guid.NewGuid(), new Money(200, Currency.USD));

        var transferService = new WalletTransferService();

        Action act = () => transferService.Transfer(fromWallet, toWallet, new Money(30, Currency.USD));

        act.Should().Throw<InsufficientFundsException>();
    }

    [Fact]
    public void Transfer_Should_Fail_When_Currencies_Do_Not_Match()
    {
        var fromWallet = new FiatWallet(Guid.NewGuid(), new Money(200, Currency.USD));
        var toWallet = new FiatWallet(Guid.NewGuid(), new Money(200, Currency.USD));

        var transferService = new WalletTransferService();

        Action act = () => transferService.Transfer(fromWallet, toWallet, new Money(30, Currency.EUR));

        act.Should().Throw<CurrencyMismatchException>();
    }

    [Fact]
    public void Transfer_Should_Fail_When_Source_And_Destination_Are_Same()
    {
        var fromWallet = new FiatWallet(Guid.NewGuid(), new Money(200, Currency.USD));

        var transferService = new WalletTransferService();

        Action act = () => transferService.Transfer(fromWallet, fromWallet, new Money(30, Currency.USD));

        act.Should().Throw<InvalidTransferException>();
    }


}