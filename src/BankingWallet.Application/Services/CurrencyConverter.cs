using BankingWallet.Domain.Exceptions;
using BankingWallet.Domain.ValueObjects;

namespace BankingWallet.Application.Services;

public static class CurrencyConverter
{
    private static readonly Dictionary<(Currency From, Currency To), decimal> ExchangeRates = new()
        {
            {(Currency.USD, Currency.EUR), 0.85m},
            {(Currency.EUR, Currency.USD), 1.18m},
            {(Currency.USD, Currency.GBP), 0.75m},
            {(Currency.GBP, Currency.USD), 1.33m},
            {(Currency.USD, Currency.BTC), 0.000022m},
            {(Currency.BTC, Currency.USD), 45000m},
            {(Currency.USD, Currency.ETH), 0.00031m},
            {(Currency.ETH, Currency.USD), 3200m},
            // Add more exchange rates as needed
        };

    public static decimal Convert(decimal amount, Currency from, Currency to)
    {
        if (from == to)
        {
            return amount;
        }

        if (ExchangeRates.TryGetValue((from, to), out var rate))
        {
            return amount * rate;
        }

        throw new InvalidTransferException();
    }
}
