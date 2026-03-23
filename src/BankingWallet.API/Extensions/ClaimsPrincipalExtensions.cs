using System.Security.Claims;
using BankingWallet.Domain.Auth.Entities;

namespace BankingWallet.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedException();
    }
}