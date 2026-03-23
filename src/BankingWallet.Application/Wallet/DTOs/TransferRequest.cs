namespace BankingWallet.Application.Wallet.DTOs;

public class TransferRequest
{
    public Guid FromWalletId { get; set; }
    public Guid ToWalletId { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
}