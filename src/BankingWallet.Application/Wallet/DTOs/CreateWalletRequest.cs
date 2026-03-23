namespace BankingWallet.Application.Wallet.DTOs;

public class CreateWalletRequest
{
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
}
