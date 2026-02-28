using BankingWallet.Domain.ValueObjects;
using BankingWallet.Domain.Exceptions;
using Xunit;
using FluentAssertions;

namespace BankingWallet.Domain.Tests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Money_Should_Equal_WhenSameAmountAndCurrency()
    {
        var m1 = new Money(10, Currency.USD);
        var m2 = new Money(10, Currency.USD);

        m1.Should().BeEquivalentTo(m2);
    }

    [Fact]
    public void Money_Add_Should_Throw_WhenCurrencyMismatch()
    {
        var usd = new Money(10, Currency.USD);
        var btc = new Money(1, Currency.BTC);

        Action act = () => usd.Add(btc);

        act.Should().ThrowExactly<CurrencyMismatchException>();
    }
}