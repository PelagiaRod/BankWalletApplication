using BankingWallet.Application.DTOs;
using BankingWallet.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace BankingWallet.Application.Tests.DTOs;

public class TransferRequestTests
{
    [Fact]
    public void TransferRequest_ValidData_ShouldCreateInstance()
    {
        // Arrange
        var fromWalletId = Guid.NewGuid();
        var toWalletId = Guid.NewGuid();
        var amount = 100.50m;
        var currency = Currency.USD;

        // Act
        var transferRequest = new TransferRequest
        {
            FromWalletId = fromWalletId,
            ToWalletId = toWalletId,
            Amount = amount,
            Currency = currency.ToString()
        };

        // Assert
        fromWalletId.Should().Be(transferRequest.FromWalletId);
        toWalletId.Should().Be(transferRequest.ToWalletId);
        amount.Should().Be(transferRequest.Amount);
        currency.ToString().Should().Be(transferRequest.Currency);
    }
}