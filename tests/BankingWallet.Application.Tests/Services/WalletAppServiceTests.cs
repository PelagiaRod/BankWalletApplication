using BankingWallet.Application.Interfaces;
using BankingWallet.Application.Services;
using BankingWallet.Domain.Entities;
using BankingWallet.Domain.Services;
using BankingWallet.Domain.ValueObjects;
using FluentAssertions;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace BankingWallet.Application.Tests.Services;

public class WalletAppServiceTests
{
    [Fact]
    public async Task Transfer_Should_CallRepositoryAndUpdateBalances()
    {
        var fromWallet = new FiatWallet(Guid.NewGuid(), new Money(200, Currency.USD));
        var toWallet = new FiatWallet(Guid.NewGuid(), new Money(100, Currency.USD));

        var walletRepoMock = new Mock<IWalletRepository>();
        walletRepoMock.Setup(r => r.GetById(fromWallet.Id)).Returns(fromWallet);
        walletRepoMock.Setup(r => r.GetById(toWallet.Id)).Returns(toWallet);

        var service = new WalletAppService(walletRepoMock.Object, new WalletTransferService());

        await service.Transfer(fromWallet.Id, toWallet.Id, new Money(50, Currency.USD));

        fromWallet.Balance.Should().BeEquivalentTo(new Money(150, Currency.USD));
        toWallet.Balance.Should().BeEquivalentTo(new Money(150, Currency.USD));

        walletRepoMock.Verify(r => r.UpdateAsync(fromWallet), Times.Once);
        walletRepoMock.Verify(r => r.UpdateAsync(toWallet), Times.Once);
        walletRepoMock.Verify(r => r.AddTransactionAsync(It.IsAny<Transaction>()), Times.Once);
    }
}