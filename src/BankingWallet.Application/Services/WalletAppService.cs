using BankingWallet.Application.Interfaces;
using BankingWallet.Domain.Services;
using BankingWallet.Domain.ValueObjects;
using BankingWallet.Domain.Entities;

namespace BankingWallet.Application.Services;

public class WalletAppService
{
    private readonly IWalletRepository _walletRepository;
    private readonly WalletTransferService _transferService;

    public WalletAppService(IWalletRepository walletRepository, WalletTransferService transferService)
    {
        _walletRepository = walletRepository;
        _transferService = transferService;
    }

    public async Task Transfer(Guid fromWalletId, Guid toWalletId, Money amount)
    {
        var fromWallet = _walletRepository.GetById(fromWalletId)
            ?? throw new InvalidOperationException("Source wallet not found");
        var toWallet = _walletRepository.GetById(toWalletId)
            ?? throw new InvalidOperationException("Target wallet not found");

        _transferService.Transfer(fromWallet, toWallet, amount);

        await _walletRepository.UpdateAsync(fromWallet);
        await _walletRepository.UpdateAsync(toWallet);

        await _walletRepository.AddTransactionAsync(new Transaction(Guid.NewGuid(), fromWalletId, amount, TransactionType.Transfer, DateTime.UtcNow));

    }
}
