using System.Security.Claims;
using BankingWallet.API.Extensions;
using BankingWallet.Application.Auth.DTOs;
using BankingWallet.Application.Auth.Interfaces;

namespace BankingWallet.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth")
            .WithTags("Auth");

        group.MapPost("/register", async (RegisterRequest request, IAuthServices authService) =>
        {
            var result = await authService.Register(request);
            return Results.Ok(result);
        })
        .AllowAnonymous();

        group.MapPost("/login", async (LoginRequest request, IAuthServices authService) =>
        {
            var result = await authService.Login(request);
            return Results.Ok(result);
        })
        .AllowAnonymous();

        group.MapPost("/refresh-token", async (RefreshTokenRequest request, IAuthServices authService) =>
        {
            var result = await authService.RefreshToken(request.RefreshToken);
            return Results.Ok(result);
        });

        group.MapPost("/logout", async (IAuthServices authService, ClaimsPrincipal user) =>
        {
            await authService.Logout(user.GetUserId());
            return Results.NoContent();
        })
        .RequireAuthorization();
    }
}