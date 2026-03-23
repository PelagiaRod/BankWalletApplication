using BankingWallet.Application.Wallet.DTOs;
using BankingWallet.Application.Wallet.Services;
using BankingWallet.Domain.Wallet.Entities;
using BankingWallet.Domain.Wallet.ValueObjects;
using BankingWallet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;

namespace BankingWallet.API.Endpoints;

public static class WalletEndpoints
{
    public static void MapWalletEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/wallet")
        .WithTags("Wallet")
        .RequireAuthorization();

        group.MapPost("/fiat", async (
            CreateWalletRequest request,
            BankingWalletDbContext db) =>
        {
            bool success = Enum.TryParse(request.Currency, out Currency currencyEnum);

            if (success)
            {
                var wallet = new FiatWallet(
                    Guid.NewGuid(),
                    new Money(request.Amount, currencyEnum));

                db.Wallets.Add(wallet);
                await db.SaveChangesAsync();

                return Results.Ok();
            }
            else
            {
                // Invalid value
                return Results.BadRequest("Invalid currency string");
            }
        });

        group.MapGet("/", async (BankingWalletDbContext db) =>
        {
            var wallets = await db.Wallets.ToListAsync();
            return Results.Ok(wallets);
        });

        group.MapPost("/transfer", async (WalletAppService service, TransferRequest request) =>
        {
            bool success = Enum.TryParse(request.Currency, out Currency currencyEnum);

            if (success)
            {
                await service.Transfer(request.FromWalletId, request.ToWalletId, new Money(request.Amount, currencyEnum));
                return Results.Ok();
            }
            else
            {
                return Results.BadRequest("Invalid currency string");
            }
        });

    }
}
