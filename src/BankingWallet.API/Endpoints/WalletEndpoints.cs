using BankingWallet.API.DTOs;
using BankingWallet.Application.DTOs;
using BankingWallet.Application.Services;
using BankingWallet.Domain.Entities;
using BankingWallet.Domain.ValueObjects;
using BankingWallet.Infrastructure.Persistence.Migrations;
using Microsoft.EntityFrameworkCore;

namespace BankingWallet.API.Endpoints;

public static class WalletEndpoints
{
    public static void MapWalletEndpoints(this WebApplication app)
    {
        app.MapPost("/wallets/fiat", async (
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

        app.MapGet("/wallets", async (BankingWalletDbContext db) =>
        {
            var wallets = await db.Wallets.ToListAsync();
            return Results.Ok(wallets);
        });

        app.MapPost("/transfer", async (WalletAppService service, TransferRequest request) =>
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
